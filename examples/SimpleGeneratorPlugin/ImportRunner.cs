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
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Api.Core;
using Zeiss.PiWeb.Api.Rest.Common.Authentication;
using Zeiss.PiWeb.Api.Rest.Dtos.Data;
using Zeiss.PiWeb.Api.Rest.HttpClient.Builder;
using Zeiss.PiWeb.Api.Rest.HttpClient.Data;
using Zeiss.PiWeb.Sdk.Import.ImportPlan;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace SimpleGeneratorPlugin;

public class ImportRunner : IImportRunner
{
    private readonly IActivityService _ActivityService;
    private readonly ImportTarget _ImportTarget;

    public ImportRunner(ICreateImportRunnerContext context)
    {
        _ActivityService = context.ActivityService;
        _ImportTarget = context.ImportTarget;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        if (_ImportTarget.Type != ConnectionType.Webservice)
        {
            _ActivityService.PostActivityEvent(EventSeverity.Error, "The import target is not supported");
            return;
        }

        try
        {
            var uri = new Uri(_ImportTarget.ServiceAddress);
            var authData = _ImportTarget.AuthData;
            using var restClient = CreateDataServiceClient(uri, authData);
            var random = new Random();

            while (!cancellationToken.IsCancellationRequested)
            {
                var value = Math.Round(random.NextDouble(), 2);

                var targetPart = await GetOrCreateTargetPart(restClient);
                var characteristic = await GetOrCreateCharacteristic(restClient, targetPart);
                await UploadMeasurement(restClient, targetPart, characteristic, value);

                _ActivityService.PostActivityEvent(EventSeverity.Info, $"Uploaded new measurement value: {value}");

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            // Do nothing
        }
    }

    public ValueTask DisposeAsync()
    {
        // Nothing to dispose
        return ValueTask.CompletedTask;
    }

    private static DataServiceRestClient CreateDataServiceClient(Uri uri, IAuthData authData)
    {
        var authenticationHandler = authData.AuthType switch
        {
            AuthType.Basic => NonInteractiveAuthenticationHandler.Basic(authData.Username, authData.Password),
            AuthType.WindowsSSO => NonInteractiveAuthenticationHandler.WindowsSSO(),
            AuthType.Certificate => NonInteractiveAuthenticationHandler.Certificate(authData.CertificateThumbprint),
            AuthType.OIDC => NonInteractiveAuthenticationHandler.OIDC(authData.ReadAndUpdateRefreshTokenAsync),
            _ => null
        };

        return new RestClientBuilder(uri)
            .SetAuthenticationHandler(authenticationHandler)
            .CreateDataServiceRestClient();
    }

    private static async Task<InspectionPlanPartDto> GetOrCreateTargetPart(DataServiceRestClient restClient)
    {
        var targetPath = PathInformation.Combine(PathInformation.Root, PathElement.Part("Random"));
        var fetchedParts = await restClient.GetParts(targetPath, depth: 0);

        if (fetchedParts.Count > 0)
            return fetchedParts[0];

        var newTargetPart = new InspectionPlanPartDto
        {
            Path = targetPath,
            Uuid = Guid.NewGuid()
        };

        await restClient.CreateParts([newTargetPart]);
        return newTargetPart;
    }

    private static async Task<InspectionPlanCharacteristicDto> GetOrCreateCharacteristic(
        DataServiceRestClient restClient,
        InspectionPlanPartDto targetPart)
    {
        var fetchedCharacteristics = await restClient.GetCharacteristics(targetPart.Path, depth: 1);
        var existingCharacteristic = fetchedCharacteristics.Where(characteristic => string.Equals(characteristic.Path.Name, "Width", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        if (existingCharacteristic != null)
            return existingCharacteristic;

        var characteristicPath = PathInformation.Combine(targetPart.Path, PathElement.Char("Width"));
        var newCharacteristic = new InspectionPlanCharacteristicDto
        {
            Path = characteristicPath,
            Uuid = Guid.NewGuid()
        };

        await restClient.CreateCharacteristics([newCharacteristic]);
        return newCharacteristic;
    }

    private static async Task UploadMeasurement(DataServiceRestClient restClient, InspectionPlanPartDto targetPart, InspectionPlanCharacteristicDto characteristic, double measuredValue)
    {
        var measurementValues = new Dictionary<Guid, DataValueDto>()
        {
            { characteristic.Uuid, new DataValueDto(measuredValue) }
        };

        var newMeasurement = new DataMeasurementDto
        {
            Uuid = Guid.NewGuid(),
            PartUuid = targetPart.Uuid,
            Time = DateTime.UtcNow,
            Characteristics = measurementValues
        };

        await restClient.CreateMeasurementValues([newMeasurement]);
    }
}
