---
layout: default
nav_order: 1
parent: Plugin fundamentals
title: Plugin structure
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
    - Flag in Projekteigenschaften, um zu markieren, dass es ein C#-Plugin ist (damit alle Abhängigkeiten in die Ausgabe gelegt werden)
--->

## Folder structure of a plugin
**manifest.json** and assemblies must be located in the root directory of the plugin folder.

![Folder structure](../../assets/images/plugin_fundamentals/1_folder.png "Folder structure")

## Auto Importer plugins folder
Plugin folders must be placed under Auto Importer **"Plugins"** folder.\
The AutoImporter only loads plug-ins from its program directory (exception is the DeveloperMode see chapter [Setup]({% link docs/getting_started/1_setup.md %})). As a rule, you therefore need admin rights to install and customize plugins.

## Start assembly
The **assemblyPath** property in manifest.json defines the start assembly. If assemblyPath is not specified, the **id** is used to determine the name of the assembly.\
The given assembly must provide a class that implements **IPlugin**, read chapter [First plugin]({% link docs/getting_started/2_first_plugin.md %}) for more information about IPlugin.

## Localization
The plugin system supports localization of the manifest file, in which the supported language abbreviations appear as additional subfolders in the "locals" subfolder and contain a manifest.json. See chapter [Localization]({% link docs/advanced_topics/1_localization.md %}) for more details.\
![Localization](../../assets/images/plugin_fundamentals/1_localization.png "Localization")