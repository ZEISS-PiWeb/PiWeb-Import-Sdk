#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Api.Core;
using Zeiss.PiWeb.Api.Rest.Common.Authentication;
using Zeiss.PiWeb.Api.Rest.Dtos.Data;
using Zeiss.PiWeb.Api.Rest.HttpClient.Builder;
using Zeiss.PiWeb.Api.Rest.HttpClient.Data;
using Zeiss.PiWeb.Sdk.Common.Logging;
using Zeiss.PiWeb.Sdk.Import.ImportPlan;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;
using Attribute = Zeiss.PiWeb.Api.Core.Attribute;

namespace Zeiss.SecondImportAutomation;

public class ImportRunner : IImportRunner
{
    // Define delay between two import loops
    private static readonly TimeSpan _interval = TimeSpan.FromSeconds(30);

    // Save ICreateImportRunnerContext for later use
    private readonly ICreateImportRunnerContext _importRunnerContext;

    // Fields for configuration items
    private readonly string _targetPartName;
    private readonly string _location;

    public ImportRunner(ICreateImportRunnerContext importRunnerContext)
    {
        _importRunnerContext = importRunnerContext;

        // Reading values of configuration items, defined in AutomationConfiguration.cs
        _targetPartName = _importRunnerContext.PropertyReader.ReadString(nameof(AutomationConfiguration.ImportPartName));
        _location = _importRunnerContext.PropertyReader.ReadString(nameof(AutomationConfiguration.WeatherLocation));
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Validate parameters
            if (string.IsNullOrWhiteSpace(_targetPartName) || string.IsNullOrWhiteSpace(_location))
            {
                _importRunnerContext.ActivityService.SetActivity(new ActivityProperties
                {
                    ActivityType = ActivityType.Suspension,
                    ShortDisplayText = "Parameter invalid",
                    DetailedDisplayText = "Target part name or location is not valid, abort."
                });

                return;
            }

            using var piWebRestClient = CreatePiWebRestClient();

            // Import loop
            while (!cancellationToken.IsCancellationRequested)
            {
                // Inform user that the plug-in is currently active
                _importRunnerContext.ActivityService.SetActivity(new ActivityProperties
                {
                    ActivityType = ActivityType.Normal,
                    DetailedDisplayText = "Fetching and storing data ..."
                });

                // Request PiWeb API and check for part
                var targetPart = await EnsurePartAsync(piWebRestClient, $"{_targetPartName}/{_location}", cancellationToken);
                var characteristic = await EnsureCharacteristicAsync(
                    piWebRestClient,
                    targetPart.Path,
                    "temperature",
                    cancellationToken);

                // Request import source
                var data = await FetchWeatherDataAsync(_location);
                if (data != null)
                {
                    // Got data, save it

                    var temperature = data[0];

                    var measurement = new DataMeasurementDto
                    {
                        Uuid = Guid.NewGuid(),
                        Time = DateTime.Now,
                        PartUuid = targetPart.Uuid,
                        Characteristics = new Dictionary<Guid, DataValueDto>()
                        {
                            { characteristic.Uuid, new DataValueDto { Attributes = [new Attribute(1, temperature)] } }
                        }
                    };

                    await piWebRestClient.CreateMeasurementValues([measurement], cancellationToken: cancellationToken);

                    _importRunnerContext.ActivityService.SetActivity(
                        new ActivityProperties
                        {
                            ActivityType = ActivityType.Normal,
                            DetailedDisplayText = "Saved data, waiting for {0} s ..."
                        }, _interval.TotalSeconds);
                }
                else
                {
                    // Error with the import source

                    _importRunnerContext.ActivityService.SetActivity(
                        new ActivityProperties
                        {
                            ActivityType = ActivityType.Suspension,
                            DetailedDisplayText = "Could not reach weather api, waiting for {0} s ..."
                        }, _interval.TotalSeconds);
                }

                // Delay next import loop, save load on the server and import source
                await Task.Delay(_interval, cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // ignore
        }
        catch (Exception ex)
        {
            _importRunnerContext.ActivityService.SetActivity(
                new ActivityProperties
                {
                    ActivityType = ActivityType.Suspension,
                    DetailedDisplayText = ex.InnerException?.Message ?? ex.Message
                }
            );
            _importRunnerContext.Logger.LogError(ex.InnerException?.Message ?? ex.Message);
        }
    }

    /// <summary>
    ///     Creates a valid PathInformation according to the specified path to a part.
    /// </summary>
    private static PathInformation AsPartPath(string partPath)
    {
        var path = PathInformation.Root;

        foreach (var element in partPath.Split('/', StringSplitOptions.RemoveEmptyEntries))
            path += PathElement.Part(element);

        return path;
    }

    /// <summary>
    ///     Ensures that the given part path is available on the PiWeb Server.
    /// </summary>
    private static async Task<InspectionPlanPartDto> EnsurePartAsync(
        DataServiceRestClient restClient,
        string path,
        CancellationToken cancellationToken)
    {
        var partPath = AsPartPath(path);

        var parts = await restClient.GetParts(partPath, depth: 0, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (parts.Count > 0)
            return parts[0];

        var part = new InspectionPlanPartDto
        {
            Uuid = Guid.NewGuid(),
            Path = partPath
        };

        await restClient.CreateParts([part], cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return part;
    }

    /// <summary>
    ///     Ensures that the characteristic under the given part is available on the PiWeb Server. 
    /// </summary>
    private static async Task<InspectionPlanCharacteristicDto> EnsureCharacteristicAsync(
        DataServiceRestClient restClient,
        PathInformation partPath,
        string characteristic,
        CancellationToken cancellationToken)
    {
        var characteristics = await restClient.GetCharacteristics(partPath, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        var knownCharacteristic = characteristics.FirstOrDefault(c => c.Path.Name == characteristic);
        if (knownCharacteristic is not null)
            return knownCharacteristic;

        knownCharacteristic = new InspectionPlanCharacteristicDto
        {
            Uuid = Guid.NewGuid(),
            Path = partPath + PathElement.Char(characteristic)
        };

        await restClient.CreateCharacteristics([knownCharacteristic], cancellationToken: cancellationToken).ConfigureAwait(false);

        return knownCharacteristic;
    }

    /// <summary>
    ///     Establishes a connection to PiWeb Server using the PiWeb API.
    /// </summary>
    private DataServiceRestClient CreatePiWebRestClient()
    {
        // Define authentication
        var authData = _importRunnerContext.ImportTarget.AuthData;

        var authenticationHandler = authData.AuthType switch
        {
            AuthType.Basic => NonInteractiveAuthenticationHandler.Basic(authData.Username, authData.Password),
            AuthType.WindowsSSO => NonInteractiveAuthenticationHandler.WindowsSSO(),
            AuthType.Certificate => NonInteractiveAuthenticationHandler.Certificate(authData.CertificateThumbprint),
            AuthType.OIDC => NonInteractiveAuthenticationHandler.OIDC(authData.ReadAndUpdateRefreshTokenAsync),
            _ => null
        };

        // Rest client for PiWeb API
        using var builder = new RestClientBuilder(new Uri(_importRunnerContext.ImportTarget.ServiceAddress))
            .SetAuthenticationHandler(authenticationHandler);

        return builder.CreateDataServiceRestClient();
    }

    /// <summary>
    ///     Uses the wttr.in http interface to retrieve weather data.
    /// </summary>
    /// <param name="location">Location name for which the weather data is to be queried.</param>
    private static async Task<string[]?> FetchWeatherDataAsync(string location)
    {
        var uri = new Uri($"https://wttr.in/{location}?format=%t;%f;%C;%h;%p;%T;%w;%P;%u");

        using var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetStringAsync(uri).ConfigureAwait(false);

            var segments = response.Split(";");
            var values = new string[10];

            values[0] = segments[0].Replace("°C", "");              // t: -1°C
            values[1] = segments[1].Replace("°C", "");              // f: -5°C
            values[2] = segments[2];                                // C: Light freezing drizzle
            values[3] = segments[3].Replace("%", "");               // h: 93%
            values[4] = segments[4].Replace("mm", "");              // p: 0.0mm
            values[5] = DateTime.Now.ToString("yyyy-MM-ddT") + segments[5];   // T: 14:17:11+0100
            values[6] = segments[6].Replace("km/h", "")[1..];       // w: →13km/h [speed]
            values[7] = segments[6].Replace("km/h", "")[..1];       // w: →13km/h [direction]
            values[8] = segments[7].Replace("hPa", "");             // P: 1027hPa
            values[9] = segments[8];                                // u: 1

            return values;
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}
