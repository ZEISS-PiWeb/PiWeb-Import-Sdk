---
layout: default
nav_order: 5
parent: Plug-in fundamentals
title: Module Import automation
---

# {{ page.title }}

<!---
Ziele:
- Hinweise zur weiteren Umsetzung des Modultyps geben (insbesondere Datenabruf und -upload)

Inhalt:
- ImportRunner beschreiben
- Datenabruf
    - Möglichkeiten beispielhaft aufzeigen
    - auf Beispielplugins verweisen
- Datenupload
    - auf PiWeb API verweisen
--->

## IImportAutomation
Represents a custom import automation provided as part of a plug-in. Custom import automations substitute the full Auto Importer import pipeline with custom logic and are available as import sources in an import plan configuration.

```c#
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

public class MyImportModule : IImportAutomation
{
    public Task<IImportRunner> CreateImportRunnerAsync(IImportRunnerContext context)
    {
        return Task.FromResult<IImportRunner>(new MyImportRunner(context));
    }
}
```

**CreateImportRunnerAsync:** Creates a new import runner instance. An import runner is first created and then executed when an import plan is using this import automation as import source is started. Each import plan is expected to use a separate import runner instance. For this reason this method must never return the same *IImportRunner* instance twice. If the import runner cannot be created (e.g. because of invalid import plan settings), a *CreateImportRunnerException* can be thrown. The created instance will be disposed after the import plan is stopped.

## IImportRunner
Is responsible for processing the cyclical import and reacting to problems and errors accordingly.

```c#
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

public sealed class ImportRunner : IImportRunner
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            var authData = context.ImportTarget.AuthData;

            var authenticationHandler = authData.AuthType switch
            {
                AuthType.Basic => NonInteractiveAuthenticationHandler.Basic( authData.Username, authData.Password ),
                AuthType.WindowsSSO => NonInteractiveAuthenticationHandler.WindowsSSO(),
                AuthType.Certificate => NonInteractiveAuthenticationHandler.Certificate( authData.CertificateThumbprint ),
                AuthType.OIDC => NonInteractiveAuthenticationHandler.OIDC( authData.ReadAndUpdateRefreshTokenAsync ),
                _ => null
            };

            using var builder = new RestClientBuilder( new Uri( context.ImportTarget.ServiceAddress ) )
                .SetAuthenticationHandler( authenticationHandler );

            using var restClient = builder.CreateDataServiceRestClient();

            var targetPath = PathHelper.String2PartPathInformation( context.PropertyReader.ReadString( "TargetPart", "/" ) );

            var targetPart = (await restClient.GetParts( targetPath, depth: 0, cancellationToken: cancellationToken )
                                .ConfigureAwait( false )).FirstOrDefault()
                            ?? throw new InvalidOperationException( $"Part '{targetPath}' does no exist" );
            
            while (!cancellationToken.IsCancellationRequested)
            {
                var measurement = new SimpleMeasurementDto
                {
                    Uuid = Guid.NewGuid(),
                    Time = DateTime.Now,
                    PartUuid = targetPart.Uuid
                };

                await restClient.CreateMeasurements( [measurement], cancellationToken ).ConfigureAwait( false );

                context.StatusService.PostImportEvent( EventSeverity.Info, "Measurement created in part '{0}'", targetPath.ToString( ) );

                await Task.Delay( TimeSpan.FromSeconds( 10 ), cancellationToken ).ConfigureAwait( false );
            }
        }
        catch( OperationCanceledException )
        {
            // ignore
        }
    }

    public ValueTask DisposeAsync()
    {
        // Close your open connections and release reserved resources
        return ValueTask.CompletedTask;
    }
}
```

**RunAsync:** Executes a custom import automation. This method is called when an import plan is started by the user. The returned Task represents the executing import automation and should not complete until the automation is explicitly stopped by the user. When the user wants to stop the import plan, the given cancellation token is canceled. After this point the returned task is expected to complete eventually, but the current import activity should be completed beforehand. This method needs to be implemented asynchronous. This means that it is expected to return a task quickly and not to block the thread at any point. Use *Task.Run(System.Action)* to run synchronous blocking code on a background thread if necessary.