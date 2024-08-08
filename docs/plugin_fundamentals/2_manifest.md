---
layout: default
nav_order: 2
parent: Plug-in fundamentals
title: Manifest
---

# {{ page.title }}

<!---
Ziele:
- Inhalt und Aufbau der Manifest-Datei erklären

Inhalt:
- einzelne Properties der Manifest-Datei
    - entsprechend Validierungslogik Anforderungen beschreiben
- auf Json-Template verweisen
- Versionierung erklären (Versionsschema, Kompatibilität)
--->

## Schema
The schema file for validation can be found at:
<!-- Schema auf github bereitstellen. unter develop extra schema ordner, dann v1 v2 -->

### Plug-in properties
The manifest describes the plug-in and its content. The following properties can be used to provide information to the Auto Importer:

**\$schema**: URL to json schema file, for validation.\
**id**: *required* The unique identifier of the plug-in.\
**title**: *required* The title of the plug-in.\
**description**: *required* The description of the plug-in.\
**assemblyPath**: The path to the plug-in assembly (pattern: .*\\.dll$).\
**localesPath**: A custom path to the plug-in's localizations. Default is the folder 'locales'.\
**contact**: The contact email address of the plug-in author.\
**homepage**: The homepage with details for the plug-in.\
**issueTracker**: The issue tracker url for the plug-in.\
**licenseName**: The licence type of the plug-in.\
**licenseUrl**: The licence url for the plug-in.\
**sourceCode**: The source code url for the plug-in.\
**documentation**: The documentation url for the plug-in.\
**version**: The self managed version of the plug-in.\
**title**: The title of the plug-in.\
**description**: The description of the plug-in.\
**provides**: *Contains an object that provides information about the module. Read below for more information.*
<!-- manifestVersion? -->

### Provides properties (module definition)
**type**: *required* ImportAutomation or ImportFormat, see [Choose your module type]({% link docs/getting_started/1_plugin_type.md %}).\
**title**: *required* The title of the module.\
**description**: *required* The description of the module.

### Minimal example
The following manifest provides minimal information:

<!-- TODO URL einsetzen -->
```json
{
  "$schema": "../schemas/manifest.schema.json",
  "id": "Zeiss.PIWEB-19199-Showcase-Readonly",
  "version": "1.0.0",
  "title": "Test-Plug-in for showcasing readonly",
  "description": "This is a plug-in which showcases the readonly functionality of plug-in UI.",

  "provides": [
    {
      "type": "ImportAutomation",
      "id": "ImportModule",
      "title": "Readonly showcase",
      "description": "This is a plug-in which showcases the readonly functionality of plug-in UI."
    }
  ]
}
```