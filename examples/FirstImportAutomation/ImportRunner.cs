#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Api.Core;
using Zeiss.PiWeb.Api.Rest.Common.Authentication;
using Zeiss.PiWeb.Api.Rest.Dtos.Data;
using Zeiss.PiWeb.Api.Rest.HttpClient.Builder;
using Zeiss.PiWeb.Sdk.Import.ImportPlan;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.FirstImportAutomation;

public class ImportRunner(ICreateImportRunnerContext context) : IImportRunner
{
    /// <summary>
    ///     Defined name for the part under which import is to take place.
    /// </summary>
    private const string TargetPartName = "FirstImportAutomationPart";

    /// <summary>
    ///     IActivityService, retrieved by ICreateImportRunnerContext for later use.
    /// </summary>
    private readonly IActivityService _statusService = context.ActivityService;

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Define authentication
            var authData = context.ImportTarget.AuthData;

            var authenticationHandler = authData.AuthType switch
            {
                AuthType.Basic => NonInteractiveAuthenticationHandler.Basic(authData.Username, authData.Password),
                AuthType.WindowsSSO => NonInteractiveAuthenticationHandler.WindowsSSO(),
                AuthType.Certificate => NonInteractiveAuthenticationHandler.Certificate(authData.CertificateThumbprint),
                AuthType.OIDC => NonInteractiveAuthenticationHandler.OIDC(authData.ReadAndUpdateRefreshTokenAsync),
                _ => null
            };

            // Rest client for PiWeb API
            using var builder = new RestClientBuilder(new Uri(context.ImportTarget.ServiceAddress))
                .SetAuthenticationHandler(authenticationHandler);

            using var restClient = builder.CreateDataServiceRestClient();

            // Target part path information
            var targetPath = PathInformation.Root;
            targetPath += PathElement.Part(TargetPartName);

            // Check existing of that part in the import loop
            while (!cancellationToken.IsCancellationRequested)
            {
                // Inform user that the plug-in is currently active
                _statusService.SetActivity(
                    new ActivityProperties()
                    {
                        ActivityType = ActivityType.Normal,
                        ShortDisplayText = "Checking PiWeb",
                        DetailedDisplayText = $"Checking PiWeb for {targetPath}"
                    }
                );

                // Request PiWeb API and check for part
                var knownParts = await restClient.GetParts(targetPath, depth: 0, cancellationToken: cancellationToken).ConfigureAwait(false);
                var targetPart = knownParts.FirstOrDefault();

                if (targetPart != null)
                {
                    // Part is known in database

                    _statusService.SetActivity(
                        new ActivityProperties()
                        {
                            ActivityType = ActivityType.Normal,
                            ShortDisplayText = "Part exists",
                            DetailedDisplayText = $"{targetPath} exists in database"
                        }
                    );
                }
                else
                {
                    // Part is unknown in database

                    _statusService.SetActivity(
                        new ActivityProperties()
                        {
                            ActivityType = ActivityType.Suspension,
                            ShortDisplayText = "Part does NOT exists",
                            DetailedDisplayText = $"{targetPath} not found in database, creating it"
                        }
                    );

                    // Create that part
                    var part = new InspectionPlanPartDto
                    {
                        Uuid = Guid.NewGuid(),
                        Path = targetPath
                    };

                    await restClient.CreateParts([part], cancellationToken: cancellationToken).ConfigureAwait(false);
                }

                // Delay next import loop, save load on the server
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException)
        {
            // Normally, the last save operation should be processed here, as the import plan has been stopped
        }
    }
}
