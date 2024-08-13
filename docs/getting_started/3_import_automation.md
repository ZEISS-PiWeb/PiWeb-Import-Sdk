---
layout: default
nav_order: 3
parent: Getting started
title: Create your first import automation
---

<!---
Ziele:
- anhand einer einfachen Beispielanwendung Schritt für Schritt das Vorgehen und die wichtigsten Themen für den Modultyp beschreiben

Inhalt:
- IImportAutomation implementieren
- Implementierung registrieren
- Implementierung im manifest eintragen
- IImportRunner implementieren (wird für jeden Run erzeugt)
- Events verwenden um zu demonstrieren, dass es läuft (auf genaue Erklärung im Kapitel "Import Monitoring" verweisen)

Notizen:
- Schema mit voller URL auf GitHub bereitstellen
--->

# {{ page.title }}
The plug-in which is created here together should be an import automation. This connects to a PiWeb Cloud instance and checks for the presence of a specific part; if this is not present, the plug-in will create it.

{: .note }
This chapter only covers the minimum functionality, to go deeper we recommend taking a look at the Plugin Fundamentals collection.

## Download source code and content
<!-- TODO zip Datei verlinken/bereistellen, bzw Code in example Ordner unter develop packen -->

The following files are required for this automation and are included in the example. *manifest.json* defines the content of our plug-in. *Plugin.cs* which represents the entry point into the plug-in, in which it registers the import automation. *ImportAutomation.cs* contains the named automation and provides the necessary import runner, which executes the specific import plans. *ImportRunner.cs* contains this IImportRunner and is responsible for the import loop.

## IPlugin
The import automation must be registered in the Auto Importer when the application is started.

**Plugin.cs:**
```c#
using Zeiss.PiWeb.Import.Sdk;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

public class Plugin : IPlugin
{
    public IImportAutomation GetImportAutomation(IImportAutomationContext context)
    {
        return new ImportAutomation();
    }
}
```

`RegisterImportAutomation`\
Registers an import automation.

## IImportAutomation
Represents a custom import automation provided as part of a plug-in. Custom import automations substitute the full Auto Importer import pipeline with custom logic and are available as import sources in an import plan configuration.

```c#
using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

namespace Zeiss.FirstImportAutomation;

public class ImportAutomation : IImportAutomation
{
    public Task<IImportRunner> CreateImportRunnerAsync(IImportRunnerContext context)
    {
        return Task.FromResult<IImportRunner>(new ImportRunner(context));
    }
}
```

`CreateImportRunnerAsync`\
Creates a new import runner instance. An import runner is first created and then executed when an import plan is using this import automation as import source is started. Each import plan is expected to use a separate import runner instance. For this reason this method must never return the same *IImportRunner* instance twice. If the import runner cannot be created (e.g. because of invalid import plan settings), a *CreateImportRunnerException* can be thrown. The created instance will be disposed after the import plan is stopped.

## IImportRunner
Is responsible for processing the cyclical import and reacting to problems and errors accordingly. In our example, the Auto Importer connects to our PiWeb Cloud instance and checks for the presence of the part named "FirstImportAutomationPart", the result is displayed to the user via Activities. In addition, the part is created if it is not found. This means that there should always be a part in the second import loop.

{: .note }
For the sake of completeness, the entire source code is presented here, followed by a step-by-step walkthrough.

```c#
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Api.Core;
using Zeiss.PiWeb.Api.Rest.Common.Authentication;
using Zeiss.PiWeb.Api.Rest.Dtos.Data;
using Zeiss.PiWeb.Api.Rest.HttpClient.Builder;
using Zeiss.PiWeb.Import.Sdk.Activity;
using Zeiss.PiWeb.Import.Sdk.ImportPlan;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

public class ImportRunner( IImportRunnerContext context ) : IImportRunner
{
    private const string TargetPartName = "FirstImportAutomationPart";

    private readonly IStatusService _StatusService = context.StatusService;

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
            while( !cancellationToken.IsCancellationRequested )
            {
                _StatusService.SetActivity(
                    new ActivityProperties()
                    {
                        ActivityType = ActivityType.Normal,
                        ShortDisplayText = "Checking PiWeb",
                        DetailedDisplayText = $"Checking PiWeb for {targetPath.ToString()}"
                    } );

                // Request PiWeb API and check for part
                var targetPart =
                    ( await restClient.GetParts( targetPath, depth: 0, cancellationToken: cancellationToken ).ConfigureAwait( false ) )
                    .FirstOrDefault();

                if( targetPart != null )
                {
                    // Part is known in database

                    _StatusService.SetActivity(
                        new ActivityProperties()
                        {
                            ActivityType = ActivityType.Normal,
                            ShortDisplayText = "Part exists",
                            DetailedDisplayText = $"{targetPath.ToString()} exists in database"
                        } );
                }
                else
                {
                    // Part is unknown in database

                    _StatusService.SetActivity(
                        new ActivityProperties()
                        {
                            ActivityType = ActivityType.Suspension,
                            ShortDisplayText = "Part does NOT exists",
                            DetailedDisplayText = $"{targetPath.ToString()} not found in database, creating it"
                        } );

                    // Create that part
                    var part = new InspectionPlanPartDto
                    {
                        Uuid = Guid.NewGuid(),
                        Path = targetPath
                    };

                    await restClient.CreateParts( [part], cancellationToken: cancellationToken ).ConfigureAwait( false );
                }

                await Task.Delay( TimeSpan.FromSeconds( 5 ), cancellationToken ).ConfigureAwait( false );
            }
        }
        catch (OperationCanceledException)
        {
            // ignore
        }
    }
}
```

**RunAsync:**\
Executes a custom import automation. This method is called when an import plan is started by the user. The returned Task represents the executing import automation and should not complete until the automation is explicitly stopped by the user. When the user wants to stop the import plan, the given cancellation token is canceled. After this point the returned task is expected to complete eventually, but the current import activity should be completed beforehand. This method needs to be implemented asynchronous. This means that it is expected to return a task quickly and not to block the thread at any point. Use *Task.Run(System.Action)* to run synchronous blocking code on a background thread if necessary.

## Running the plug-in
To check that the plug-in works, we install it as described in chapter [Starting a plug-in]({% link docs/setup/1_import_destination.md %}). The plug-in management view should then look like this:\
![Installed plug-in](../../assets/images/getting_started/3_management_view.png "Installed plug-in")

We also need an import plan that uses our import source and uses the existing cloud instance as the target:\
![Import plan](../../assets/images/getting_started/3_import_plan.png "Import plan")

If we now click on Start, the automation is executed and after the third import loop it should look like this:\
![Finished plug-in](../../assets/images/getting_started/3_finished.png "Finished plug-in")