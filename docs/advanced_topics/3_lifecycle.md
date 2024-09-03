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
The lifecycle defines when which class is instantiated by the Auto Importer and which method is called. This can affect the program start but also the creation and execution of import plans and the file import process. In the following the lifecycle for import automation and import format plug-ins is described.

## Import automation

### 1. Auto Importer application starting
All available plug-ins are loaded when the application is started. To do this, the `IPlugin` implementation of the import automation plug-in is called up using the `CreateImportAutomation` method.

```c#
using Zeiss.PiWeb.Import.Sdk;

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
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

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

In general, an import format plug-in is loaded in the Auto Importer in similar way as an import automation plug-in. In contrast to the import automation plug-in, however, the import group builder and the import parser of the import format plug-in are integrated into the existing import process of the Auto Importer. The basic differences between the two plug-in types can be found in [Plug-in type]({% link docs/plugin_fundamentals/2_plugin_type.md %}).

### 1. Load plug-in
An import format plug-in is loaded when the Auto Importer is started. To do this, the `CreateImportFormat` method of the `IPlugin` implementation is called.

```c#
using Zeiss.PiWeb.Import.Sdk;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

public class Plugin : IPlugin
{
    public IImportFormat CreateImportFormat(ICreateImportFormatContext context)
    {
        return new ImportFormat();
    }
}
```

### 2. Get import format configuration
During the start of the Auto Importer the defined import format configuration for the plug-in is created. Therefore the `CreateConfiguration` method of the `IImportFormat` implementation is called and an `ImportFormatConfiguration` instance is created.

```c#
public IImportFormatConfiguration CreateConfiguration(ICreateImportFormatConfigurationContext context)
{
    return new ImportFormatConfiguration();
}
```

Accordingly the import configuration for the import format of the plug-in can be displayed in the import configuration view of the running Auto Importer.

### 3. Create and use import group builder
The import process starts when an import plan of the Auto Importer is running and a new import file is detected in the specified import folder. Alternatively, the import process for a file also starts when an example file in the import configuration view is selected. At the beginning of the import process the Auto Importer creates an import group filter for each file format and uses this filters to decide to which format the import file belongs. To create the import group filter of the plug-in format the `CreateImportGroupFilter` method of `IImportFormat` implementation is called.

```c#
public IImportGroupFilter CreateImportGroupFilter(ICreateImportGroupFilterContext context)
{
    return new ImportGroupFilter();
}
```

For each interval when the Auto Importer checks for new files in the import folder a new import group filter is created.  
Immediately afterwards, the method `FilterAsync` of the `IImportGroupFilter` implementation is called in order to check whether the file of the import group belongs to the import format.

```c#
using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.ImportFiles;

public class ImportGroupFilter : IImportGroupFilter
{
    public async ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context)
    {
        [...]
    }
}
```

### 4. Create and use import parser
After an import group is assigned to an import format via the import group builder the import group is processed by the import parser. The import parser is created by calling the `CreateImportParser` method of `IImportFormat` implementation. A new instance for each parsing of an import group is created.

```c#
public IImportParser CreateImportParser(ICreateImportParserContext context)
{
    return new ImportParser();
}
```

After the creation of the import parser the `ParseAsync` method of the import parser is called to start the parsing of the information in the import file and creating the inspection plan and measurement data.

```c#
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.ImportData;
using Zeiss.PiWeb.Import.Sdk.ImportFiles;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

public class ImportParser : IImportParser
{
    public async Task<ImportData> ParseAsync(
        IImportGroup importGroup,
        CancellationToken cancellationToken,
        IParseContext context)
    {
        [...]
    }
}
```