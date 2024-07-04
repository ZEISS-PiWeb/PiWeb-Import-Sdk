---
layout: default
nav_order: 5
parent: Getting started
title: Create your first import automation
---

# {{ page.title }}

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

This chapter describes what is required to create a minimal automation plug-in. Please read [Create your first plugin]({% link docs/getting_started/2_first_plugin.md %}) before, as it extends the information from there.

## IImportAutomation
Represents a custom import automation provided as part of a plugin. Custom import automations substitute the full Auto Importer import pipeline with custom logic and are available as import sources in an import plan configuration.

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
    public ImportRunner(IImportRunnerContext context)
    {
        // Read stuff needed for the import loop from context here
        // E.g. configuration properties from storage
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Place your import loop here    
        }
        catch( OperationCanceledException )
        {
            // Happens when user stops the import plan, finish last import
        }
        catch
        {
            // Error handling and shutdown
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