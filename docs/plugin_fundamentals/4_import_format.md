---
layout: default
nav_order: 4
parent: Plug-in fundamentals
title: Import format plug-ins
---

# {{ page.title }}

<!---
Ziele:
- Hinweise zur Implementierung des Modultyps geben (insbesondere Dateigruppierung, Parsen, Prüfplan bauen)

Inhalt:
- Implementierung von IImportFormat beschreiben
- File grouping
    - Beschreibung der GetGroup-Methode
    - insbesondere Status-Enum erklären
- Parsing & building inspection plan
    - Möglichkeiten zum Lesen der Datei 
    - Erstellung eines TreeDataImage erklären
        - welche Entitäten können belegt werden
        - AttributeTemplates erklären
--->

This article describes the implementation of an import format plug-in. A general explanation of this type can be found in [Plug-in type]({% link docs/plugin_fundamentals/3_plugin_type.md %}). The focus in this arcticle is on the implementation of the grouping and parsing functionality. The required information for this plug-in type in the manifest.json file is described in [Manifest]({% link docs/plugin_fundamentals/2_manifest.md %}). The development of a simple example for an import format plug-in can be found in [Create your first import format]({% link docs/advanced_topics/5_project_template.md %}).

## Implementing IImportFormat
The interface `IImportFormat` represents the import format that should be added as a new format for the Auto Importer via the plug-in. Each import format plug-in needs an implementation of the `IImportFormat` interface. An instance of the implementation of this interfaces has to be returned in the method `CreateImportFormat` of the class which implements the `IPlugin` interface like in the following example.

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

Like in the following example of an implementation of the `IImportFormat` interface the method `CreateImportGroupFilter` for the file grouping functionality and the method `CreateImportParser` for the parsing functionality has to be implemented. In addition an implementation for the method `CreateConfiguration` to replace the default import format settings can be defined.

```c#
using Zeiss.PiWeb.Import.Sdk.ImportFiles;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

public sealed class ImportFormat : IImportFormat
{
    public IImportGroupFilter CreateImportGroupFilter(ICreateImportGroupFilterContext context)
    {
        return new ImportGroupFilter();
    }

    public IImportParser CreateImportParser(ICreateImportParserContext context)
    {
        return new ImportParser();
    }

    public IImportFormatConfiguration CreateConfiguration(ICreateImportFormatConfigurationContext context)
    {
        return new ImportFormatConfiguration();
    }
}
```

## Grouping import files
The Auto Importer needs to know which files in the import folder belongs to which import format. Therefore each import format needs an import group filter. The Auto Importer pass an import file to the import group filter of each import format. The filter should decide whether the file belongs to the format and how it should be handled. This functionality is implemented in the method `FilterAsnyc` of the `IImportGroupFilter`. A simple example of an import filter could look like follows:

```c#
public class ImportGroupFilter : IImportGroupFilter
{
    public async ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context)
    {
        // Check file extension.
        if (!importGroup.PrimaryFile.HasExtension(".txt"))
            return FilterResult.None;
            
        return FilterResult.Import;
    }
}
```

The initial import group that is passed to the `FilterAsync` method consists only of one import file. The import file can be checked in the method to see which format it belongs to. In the example it is checked whether the file extension matches the expected extension. In addition, the file content could also be analyzed to see if it corresponds to the format.

### Returning a filter result
Depending on the validation, a `FilterResult` is returned for the import file respectively the import group. The `FilterResult`is an enum with the following values:

```c#
public enum FilterResult
{
    None,           // ImportGroup should not be imported by this module (wrong file/format)
    Import,         // ImportGroup is to be imported, all necessary data is available
    Discard,        // ImportGroup is invalid, the files should be discarded directly
    RetryOrImport,  // ImportGroup is to be imported, but we are still waiting for possible additional files, e.g. additional data
    RetryOrDiscard, // ImportGroup should not be imported, but there may still be files that make importing possible
}
```

When the import file does not belong to the format then `None`is returned. In this case the Auto Importer passes the import group to the ipmort group filter of the next format. `Import` is used when the import file belongs to the format and the import group can be imported immediatly. `Discard` means that the import group belongs to the import format but something in the group is permanently defect. When a import group is marked with `Discard`the files in the group are deleted and no longer considered for the import. However, when a defect import group should not be deleted immediatly the filter result `RetryOrDiscard` can be used. Then the import group will not be taken into account for the current filter process but after some time this import group will be filtered again. The last possible result value is `RetryOrImport`. This value can be used to signal that the import group belongs to the format but it should not immediately be imported because some optional dependencies are still missing. This import group will only be considered again later. And if nothing has changed in the import group after a certain time, it is imported. 

### Add additional files to the import group
The optional dependencies for an import group could be additional import files besides the primary import file when the measurement information to be imported is divided across several files. Or there could be data like images or documents that should be imported as additional data for inspection plan entities in PiWeb. The following example shows how additional files could be added to the presented import group in the `FilterAsync` method.

```c#
public async ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context)
{
    // Check file extension.
    if (!importGroup.PrimaryFile.HasExtension(".txt"))
        return FilterResult.None;

    // Add additional data to import group.
    var additionalData = context.CurrentImportFolder.FindFile(importGroup.PrimaryFile.BaseName + ".png");
    if (additionalData == null)
        return FilterResult.RetryOrImport;
    importGroup.AddFile(additionalData);
            
    return FilterResult.Import;
}
```

As shown in the example, the `IFilterContext` can be used to search for files in the import folder. In the example an image with the same file name like the import file is searched for and then added to the import group.

### Priorisation of import formats
As mentioned at the beginning each import format has to define an import group filter. The Auto Importer can then ask all import formats in sequence whether an import file belongs to the respective format. Once an import group filter returns as a filter result that the primary file of the import group belongs to the format, the filter methods of the subsequent formats are no longer called for this import group.  
The order in which the import formats are asked by the Auto Importer depends on the priority of the import format. Each built-in import format is already assigned a priority. For import format plug-ins, the priority can be set with the `priority` property in the manifest file (see [Manifest]({% link docs/plugin_fundamentals/2_manifest.md %})). Built-in formats have a priority value between 100 and 1000. Accordingly, the numerical value for the priority of the import format plug-in can be selected as follows:
* If the priority of a format is less than 100 then it is able to handle import files before the built-in formats.
* If the priority of a format is greater than 1000 the format will only be used if no built-in format handles the file.
* If no priority value is defined in the manifest the default priority of 50 is used.

## Parsing import files
When an import group has been assigned to a format by the import filter the Auto Importer passes the import group to the corresponding import parser. The parser is responsible for parsing import files and creating inspection plan data, measurement data and additional data which can be imported to the PiWeb database. Therefore each import format plug-in needs an implementation of the `IImportParser` interface with the method `ParseAsync`. The following example will be used to explain what has to be implemented in this method.

```c#
public async Task<ImportData> ParseAsync(
    IImportGroup importGroup,
    CancellationToken cancellationToken,
    IParseContext context)
{
    // Create root part and measurement.
    var root = new InspectionPlanPart(importGroup.PrimaryFile.BaseName);
    var measurement = root.AddMeasurement();

    // Create reader for import file.
    await using var stream = importGroup.PrimaryFile.GetDataStream();
    using var reader = new StreamReader(stream);

    string? line;

    // Parse header attributes.
    while ((line = reader.ReadLine()) != null)
    {
        if( string.IsNullOrEmpty(line))
            continue;

        if (line.StartsWith("#Characteristic"))
            break;

        var rowItems = line.Split(": ");
        var attribute = rowItems[0].Trim();
        var value = rowItems[1].Trim();

        switch (attribute)
        {
            case "Date":
                measurement.SetAttribute(4,value);
                break;
            case "Operator":
                measurement.SetAttribute(9,value);
                break;
        }
    }

    // Parse measured value for each characteristic.
    while ((line = reader.ReadLine()) != null)
    {
        if( string.IsNullOrEmpty(line))
            continue;
                
        var rowItems = line.Split(',');
        var characteristicName = rowItems[0].Trim();
        var value = rowItems[1].Trim();
                
        var characteristic = root.AddCharacteristic(characteristicName);
        var measuredValue = measurement.AddMeasuredValue(characteristic);
        measuredValue.SetAttribute(1,double.Parse(value));
    }

    // Add additional data.
    foreach (var rawData in importGroup.AdditionalFiles)
        measurement.AddAdditionalData(rawData.Name, rawData.GetDataStream());

    return new ImportData(root);
}
```

### Read information from the import file
The files in an import group are represented as instances of the implementation of `IImportFile`. Besides properties for file name and extension such instances provides the method `GetDataStream`. Like in the example a `StreamReader` can be created with this method and can then be used to read the information in the import file.

```c#
await using var stream = importGroup.PrimaryFile.GetDataStream();
using var reader = new StreamReader(stream);

string? line;

while ((line = reader.ReadLine()) != null)
{
    // Read the file information
}
```

The information in the file can then be read out line by line, for instance, and divided into separate information pieces. The information pieces can be used to create inspection plan and measurement data.

### Create inspection plan entities
When parsing an import file, the goal is to transfer the information from the file to the structure in the PiWeb database. In PiWeb, we distinguish between the inspection plan consisting of parts and characteristics and the measurement, which is assigned to a part and contains measured values for characteristics.  
An inspection plan has a tree structure. On top is the root part which can have further parts as children. Parts can have sub-parts or sub-characteristics. Characteristics, however, can only have sub-characteristics.  
An instance of the `ImportData` class has to be created in the `ParseAsync` method that represents the data to be imported. An `ImportData` object contains a root part which is the root of the inspection plan data, measurement data and additional data to be imported. During import this part will be merged with the designated import part as configured by the import plan and optionally the path rule configuration. A root part as well as a part is an instance of the `InspectionPlanPart` class. 

```c#
var root = new InspectionPlanPart(importGroup.PrimaryFile.BaseName);
```

Starting from the root part the inspection plan for the import can be created. Although when only a measurement and no new inspection plan data should be imported an inspection plan has to be created in the parser to define to which part the measurement and to which characteristics the measured values are assigned. Parts and characteristics can be created as follows.

```c#
// Create root part and sub-part.
var root = new InspectionPlanPart("RootPart");
var part = root.AddPart("Part");
// Create a characteristics.
var characteristic = part.AddCharacteristic("Characteristic");
```

The names of the inspection plan entities are fixed in this example. When importing a file, the names are usually read from the import file, as in the example at the beginning of this chapter.

### Create measurement entities
When measured values should be imported at first a measurement as an instance of the `Measurement` class has to be created and has to be assigned to a part like in the following example.

```c#
var measurement = root.AddMeasurement();
```

Measured values as instances of the `MeasuredValue` class can then be added to the measurement. The characteristic to which the measured value belongs must be specified.

```c#
// Read characteristic name.
var rowItems = line.Split(',');
var characteristicName = rowItems[0].Trim();

// Create measured value.
var characteristic = root.AddCharacteristic(characteristicName);
var measuredValue = measurement.AddMeasuredValue(characteristic);
```

So far, only the measured value entity has been created. The real value must still be added as an attribute value of the measured value as described below.

### Set attribute values directly
Information of the import file like the measurement date or the operator name can be imported as attribute value. In PiWeb each entity can have several attributes. Attributes are defined by their key and their data type. An attribute value can have the data type string, integer, floating point, data or catalog. The keys and the data types can be defined in the PiWeb database via the PiWeb Planner.  
In the following example, the measurement date `value` from the import file is saved as a measurement attribute with the key `4`.

```c#
measurement.SetAttribute(4,value);
```

The measured value can be imported by setting the attribute value for the attribute with key `1` of the measured value entity. In the following example the variable `value` is used for the measured value in the import file.

```c#
measuredValue.SetAttribute(1,double.Parse(value));
```

In the same way, attribute values can also be defined for parts and characteristics.

### Define variables for attribute values
The information from the import file does not have to be assigned to a specific attribute directly in the parser. Instead, the information can be saved as a variable value. This allows the user of the Auto Importer to define later in the import configuration to which attribute the variable value is mapped.  
To define a variable the method `SetVariable` can be used that exists for each entity class in PiWeb. The value of the import file is assigned to a variable name, as in the following example, the measurement date `value` is assigned to the variable with the name `Date`.

```c#
measurement.SetVariable("Date",value);
```

The variable does not only apply to the entity to which it is assigned. If the variable used for an attribute mapping is not found for the current entity, the variable is also searched for in entities that correspond to this entity. For a measured value, the variable is therefore also searched for in the related measurement and the related characteristic. The search is then also continued in the corresponding part of the measurement and in potentially existing parent parts. For a measurement, a search is also made in the related part and its parent parts. In the case of characteristics, a search is also done on parent characteristics and parts. For parts, the parent parts are considered as well.

### Import additional data
If not only the information within an import file should be imported, additional data such as images and text files can be imported into PiWeb as additional data. In PiWeb each entity can have additional data. To import such data the files have to be added to the import group as additional files during the grouping mechanism like described above. In the `ParseAsync` method the files of the import group can then be added to the entities like in the following example.

```c#
foreach (var rawData in importGroup.AdditionalFiles)
    measurement.AddAdditionalData(rawData.Name, rawData.GetDataStream());
```