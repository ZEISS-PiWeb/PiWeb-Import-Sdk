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

This chapter describes what is required to create a minimal automation plug-in.

## Manifest
```json
{
  "$schema": "../schemas/manifest.schema.json",
  "id": "Plugin1816",
  "version": "1.0.0",
  "title": "Title for the plugin",
  "description": "This is a plugin, which is used for showing the manifest in the documentation.",
  "provides": [
    {
      "type": "ImportAutomation",
      "id": "ImportAutomation1888",
      "title": "Module title",
      "description": "Import automation module description."
    }
  ]
}
```
The manifest describes your plugin content and defines the modules which are provided.

### Plugin information
**$schema:** The schema we provide to validate your manifest.\
**id:** The unique identifier for this plugin. The plugin identifier must be unique overall installed plugins.\
**version:** Your managed version of the plugin.\
**title:** Title shown in plugin management view of Auto Importer.\
<!---Bild einfügen vom plugin management view.-->
**description:** Description shown in plugin management view of Auto Importer.\
**provides:** Collection of module information.

### Module information
**type:** ImportAutomation or ImportFormat, see chapter [Module types]({% link docs/getting_started/3_module_type.md %}) for further information.\
**id:** Unique identifer for this module, must be unique within the plugin.\
**title:** Title shown in the module selection of plugin management view of Auto Importer.\
**description:** Description shown in the module selection of plugin management view of Auto Importer.

## IPlugin and IPluginContext
IPlugin represents a PiWeb Auto Importer plugin. Here you register your modules with the Auto Importer.

```c#
using Zeiss.PiWeb.Import.Sdk;

public class Plugin : IPlugin
{
    public Task Init(IPluginContext context)
    {
        context.RegisterImportAutomation("ImportAutomation19285", new ImportAutomation());

        return Task.CompletedTask;
    }
}
```

**Init:** Initializes the plugin. Usually called during startup of the hosting application while showing a splash screen. Startup finishes when the returned task is completed. *RegisterImportAutomation* on *IPluginContext* is used to register your defined module with the system.

{: .important }
> The *id* of *RegisterImportAutomation* and *RegisterImportFormat* from *IPluginContext* needs to be identical with your manifest.

## IImportAutomation
Represents a custom import automation provided as part of a plugin. Custom import automations substitute the full Auto Importer import pipeline with custom logic and are available as import sources in an import plan configuration.

```c#
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

public class ImportAutomation : IImportAutomation
{
    public Task<IImportRunner> CreateImportRunnerAsync(IImportRunnerContext context)
    {
        return Task.FromResult<IImportRunner>(new ImportRunner(context));
    }

    public IAutomationConfiguration CreateConfiguration(IAutomationConfigurationContext context)
    {
        return new AutomationConfiguration(context.PropertyStorage);
    }
}
```

**CreateImportRunnerAsync:** Creates a new import runner instance. An import runner is first created and then executed when an import plan is using this import automation as import source is started. Each import plan is expected to use a separate import runner instance. For this reason this method must never return the same *IImportRunner* instance twice. If the import runner cannot be created (e.g. because of invalid import plan settings), a *CreateImportRunnerException* can be thrown. The created instance will be disposed after the import plan is stopped.

**CreateConfiguration:** Creates a new automation configuration instance. An automation configuration is first created when an import plan uses this import automation as import source. Each import plan is expected to use a separate automation configuration instance. For this reason this method must never return the same *IAutomationConfiguration* instance twice.

## IAutomationConfiguration
Represents an automation configuration. Automation configurations specify additional settings for an import automation an user can edit to affect the import automation behavior for an import plan.

```c#
using Zeiss.PiWeb.Import.Sdk.ConfigurationItems;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;
using Zeiss.PiWeb.Import.Sdk.PropertyStorage;

public class AutomationConfiguration(IPropertyStorage storage) : IAutomationConfiguration
{
    private static readonly Section _ItemSection = new() {Title = "Test section", Priority = 0};

    [ConfigurationItem]
    private BoolConfigurationItem BoolItem { get; } = new BoolConfigurationItem(storage, "BoolDisable", true)
    {
        Title = "Use default UI texts",
        Section = _ItemSection,
        Tooltip = "If this item is checked the status item provides no text and the default text is used",
        Priority = 1
    };

    public void Update()
    {
        // Here you can react to updates of configuration items.
    }
}
```
<!---
**TODO Priority erklären, niedirg zu hoch**
**TODO Link zu allen verfügbaren IConfigurationItems (plugin/fundamentals/6 ?)**
--->

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
