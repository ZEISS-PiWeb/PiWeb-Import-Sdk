---
layout: default
nav_order: 1
parent: Plug-in fundamentals
title: Plug-in structure
---

<!---
Ziele:
- allgemeinen Aufbau eines Plug-ins beschreiben

Inhalt:
- Grundstruktur
    - Ordner mit Manifest und Assemblies
- wo sucht AI nach Plug-ins
- Manifest referenziert das Einstiegs-Assembly (direkt oder indirekt)
- Einstiegs-Assembly muss Implementierung von IPlugin haben
- Manifest-Übersetzung in Unterordnern erwähnen
- C#-Projektdatei beschreiben
    - SDK-NuGet referenzieren
    - WICHTIG: Referenz auf SDK-NuGet braucht Flag, um nicht in die Ausgabe kopiert zu werden
    - Flag in Projekteigenschaften, um zu markieren, dass es ein C#-Plug-in ist (damit alle Abhängigkeiten in die Ausgabe gelegt werden)
--->

# {{ page.title }}

## Folder structure of a plug-in
`manifest.json` and assemblies must be located in the root directory of the plug-in folder.

![Folder structure](../../assets/images/plugin_fundamentals/structure/folder.png "Folder structure")

## Auto Importer plug-ins folder
Plug-in folders must be placed under Auto Importer `"Plugins"` folder.\
The AutoImporter only loads plug-ins from its program directory (exception is the DeveloperMode see [PiWeb Auto Importer]({% link docs/setup/piweb_auto_importer.md %}#plug-in-search-paths)). As a rule, you therefore need admin rights to install and customize plug-ins.

## Start assembly
The `assemblyPath` property in manifest.json defines the start assembly. If assemblyPath is not specified, the `id` is used to determine the name of the assembly. The given assembly must provide a class that implements `IPlugin`, read [Import automation]({% link docs/plugin_fundamentals/import_automation.md %}#iplugin) for more information about IPlugin.

## Localization
The plug-in system supports localization of the manifest file, in which the supported language abbreviations appear as additional subfolders in the "locals" subfolder and contain a manifest.json. See [Localization]({% link docs/advanced_topics/localization.md %}) for more details.\
![Localization](../../assets/images/plugin_fundamentals/structure/localization.png "Localization")

## Project settings
### Nuget
The Nuget import SDK provided by us is required for plug-in development. Please integrate this into your project:\
`<PackageReference Include="Zeiss.PiWeb.Sdk.Import" Version="1.0.0" Private="false" ExcludeAssets="runtime"/>`

### Project file
The reference to the Import SDK-NuGet needs a flag to not be copied to the output, see above.\
Also make sure to mark the project as a C# plug-in (so that all dependencies appear in the output).