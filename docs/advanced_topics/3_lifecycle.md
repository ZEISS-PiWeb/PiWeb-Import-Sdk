---
layout: default
nav_order: 3
parent: Advanced topics
title: Lifecycle
---

<!---
Ziele:
- aufzeigen, wann was instanziiert wird

Inhalt:
- wann werden Instanzen beim Auto Importer erzeugt und wann disposed
    - IPlugin, ImportRunner, ImportModule etc.
--->

# {{ page.title }}
The lifecycle describes when which class is instantiated by the Auto Importer and which method is called. This can affect the program start but also the creation and execution of import plans.

## Import automation
### 1. Auto Importer application starting
All available plug-ins are loaded when the application is started. To do this, the `IPlugin` implementation of the plug-in is called up using the `CreateImportAutomation` method.

```c#
using Zeiss.PiWeb.Sdk.Import;

public class Plugin : IPlugin
{
    public IImportAutomation CreateImportAutomation(ICreateImportAutomationContext context)
    {
        return new MyImportAutomation();
    }
}
```

### 2. CreateConfiguration in IImportAutomation
The configuration of the registered import automation is then called up, to get custom configuration items defined by your plug-in. The constructor (`ImportConfiguration`) of your `IAutomationConfiguration` is called.

```c#
public IAutomationConfiguration CreateConfiguration(ICreateAutomationConfigurationContext context)
{
    return new ImportConfiguration(context.PropertyStorage);
}
```

{: .note }
This is also executed when the import source is changed to your plug-in. This is necessary to load the self-defined configuration elements.

### 3. Auto Importer application is up and running
The application is now executed as before, the plug-in is loaded and the import source can be selected.

### 4. Starting an import plan with a custom import source
Starting an import plan causes an `IImportRunner` of the selected import source to be initialized. This is done via the `CreateImportRunner` method in your `IImportAutomation` implementation.

```c#
public IImportRunner CreateImportRunner(ICreateImportRunnerContext context)
{
	return new MyImportRunner(context);
}
```

Constructor (`MyImportRunner`) of your `IImportRunner` is called. You should provide the values set by the configuration items. Read [User configuration & storage]({% link docs/plugin_fundamentals/6_configuration.md %}) for information about storage usage.

```c#
private readonly ICreateImportRunnerContext _ImportRunnerContext;

private readonly string _Hostname;
private readonly int _Port;

public MyImportRunner(ICreateImportRunnerContext importRunnerContext)
{
    _ImportRunnerContext = importRunnerContext;

    _Hostname = _ImportRunnerContext.PropertyReader.ReadString(nameof(ImportConfiguration.Hostname));
    _Port = _ImportRunnerContext.PropertyReader.ReadNumber(nameof(ImportConfiguration.Port), 1883);
}
```

### 5. RunAsync from IImportRunner is called
After initializing the `IRunner`, the import loop is started via `RunAsync`.

```c#
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

public sealed class MyImportRunner : IImportRunner
{
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Place your import loop here
        }
        catch(OperationCanceledException)
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

## Import format
