---
layout: default
nav_order: 1
parent: Plug-in fundamentals
title: Plug-in structure
---

# {{ page.title }}

<!---
Ziele:
- allgemeinen Aufbau eines Plugins beschreiben

Inhalt:
- Grundstruktur
    - Ordner mit Manifest und Assemblies
- wo sucht AI nach Plugins
- Manifest referenziert das Einstiegs-Assembly (direkt oder indirekt)
- Einstiegs-Assembly muss Implementierung von IPlugin haben
- Manifest-Übersetzung in Unterordnern erwähnen
- C#-Projektdatei beschreiben
    - SDK-NuGet referenzieren
    - WICHTIG: Referenz auf SDK-NuGet braucht Flag, um nicht in die Ausgabe kopiert zu werden
    - Flag in Projekteigenschaften, um zu markieren, dass es ein C#-Plug-in ist (damit alle Abhängigkeiten in die Ausgabe gelegt werden)
--->

## Folder structure of a plug-in
**manifest.json** and assemblies must be located in the root directory of the plug-in folder.

![Folder structure](../../assets/images/plugin_fundamentals/1_folder.png "Folder structure")

## Auto Importer plugins folder
Plug-in folders must be placed under Auto Importer **"Plugins"** folder.\
The AutoImporter only loads plug-ins from its program directory (exception is the DeveloperMode see chapter [Setup]({% link docs/getting_started/1_setup.md %})). As a rule, you therefore need admin rights to install and customize plugins.

## Start assembly
The **assemblyPath** property in manifest.json defines the start assembly. If assemblyPath is not specified, the **id** is used to determine the name of the assembly.\
The given assembly must provide a class that implements **IPlugin**, read chapter [First plug-in]({% link docs/getting_started/2_first_plugin.md %}) for more information about IPlugin.

## Localization
The plug-in system supports localization of the manifest file, in which the supported language abbreviations appear as additional subfolders in the "locals" subfolder and contain a manifest.json. See chapter [Localization]({% link docs/advanced_topics/1_localization.md %}) for more details.\
![Localization](../../assets/images/plugin_fundamentals/1_localization.png "Localization")

## Project settings
### Nuget
The Nuget import SDK provided by us is required for plug-in development. Please integrate this into your project:
<!-- NuGet Link, off. Repo? -->

### Project file
The reference to the Import SDK-NuGet needs a flag to not be copied to the output.\
Also make sure to mark the project as a C# plug-in (so that all dependencies appear in the output).