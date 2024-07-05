---
layout: default
nav_order: 2
parent: Plugin fundamentals
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
<!-- Schema auf github bereitstellen. -->
The schema file for validation can be found at TODO.

### Plugin properties
**\$schema**: URL to json schema file, for validation.\
**id**: *required* The unique identifier of the plugin.\
**title**: *required* The title of the plugin.\
**description**: *required* The description of the plugin.\
**assemblyPath**: The path to the plugin assembly (pattern: .*\\.dll$).\
**localesPath**: A custom path to the plugin's localizations. Default is the folder 'locales'.\
**contact**: The contact email address of the plugin author.\
**homepage**: The homepage with details for the plugin.\
**issueTracker**: The issue tracker url for the plugin.\
**licenseName**: The licence type of the plugin.\
**licenseUrl**: The licence url for the plugin.\
**sourceCode**: The source code url for the plugin.\
**documentation**: The documentation url for the plugin.\
**version**: The version of the plugin.\
**title**: The title of the plugin.\
**description**: The description of the plugin.\
**provides**: Contains an object that provides information about the module.

### Provides properties
**type**: *required* ImportAutomation or ImportFormat, see [Choose your module type]({% link docs/getting_started/3_module_type.md %}).\
**title**: *required* The title of the module.\
**description**: *required* The description of the module.