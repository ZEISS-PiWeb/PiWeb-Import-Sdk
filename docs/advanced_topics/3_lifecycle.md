---
layout: default
nav_order: 3
parent: Advanced topics
title: Lifecycle
---

# {{ page.title }}

<!---
Ziele:
- aufzeigen, wann was instanziiert wird

Inhalt:
- wann werden Instanzen beim Auto Importer erzeugt und wann disposed
    - IPlugin, ImportRunner, ImportModule etc.
--->

## Import Automation

**1.** Auto Importer application starting\
Calling **Init** from **IPlugin** to register your module.
```c#
using Zeiss.PiWeb.Import.Sdk;

public class Plugin : IPlugin
{
    public Task Init(IPluginContext context)
    {
        context.RegisterImportAutomation("MyImportModule", new MyImportModule());

        return Task.CompletedTask;
    }
}
```

**2.** **CreateConfiguration** in **IImportAutomation**\
CreateConfiguration is called to get custom configuration items defined by your module.
```c#
public IAutomationConfiguration CreateConfiguration( IAutomationConfigurationContext context )
{
    return new ImportConfiguration( context.PropertyStorage );
}
```

The constructor (ImportConfiguration) of your **IAutomationConfiguration** is called.

> [!NOTE]
> This is also executed when the import source is switched to a module.

**3.** Auto Importer application is up and running\
The application is now executed as before, the plug-in is loaded and the module can be selected in the custom import sources.

**4.** Starting an import plan with a custom import source\
**CreateImportRunnerAsync** inside **IImportAutomation** is called.
```c#
public Task<IImportRunner> CreateImportRunnerAsync( IImportRunnerContext context )
{
	return Task.FromResult<IImportRunner>( new MyImportRunner( context ) );
}
```

Constructor of your **IImportRunner** is called. You should provide the values set by the configuration items. Read chapter [User configuration & storage]({% link docs/plugin_fundamentals/6_configuration.md %}) for information about storage usage.
```c#
private readonly IImportRunnerContext _ImportRunnerContext;

private readonly string _Hostname;
private readonly int _Port;

public MyImportRunner( IImportRunnerContext importRunnerContext )
{
    _ImportRunnerContext = importRunnerContext;

    _Hostname = _ImportRunnerContext.PropertyReader.ReadString( nameof( ImportConfiguration.Hostname ) );
    _Port = _ImportRunnerContext.PropertyReader.ReadNumber( nameof( ImportConfiguration.Port ), 1883 );
	}
```

**5.** **RunAsync** from **IImportRunner** is called\
Your import loop is up and running.
```c#
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

public sealed class MyImportRunner : IImportRunner
{
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