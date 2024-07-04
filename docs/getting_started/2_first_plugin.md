---
layout: default
nav_order: 2
parent: Getting started
title: Create your first plugin
---

# {{ page.title }}

<!---
Ziele:
- mit sehr einfachem Beispiel erste Schritte für Pluginentwicklung aufzeigen

Inhalt:
- C#-Project Template für Plugins nutzen
    - Kurzbeschreibung Manifest-Inhalt und Anpassungen vornehmen (z.B. ID, Autor)
    - IPlugin Implementierung beschreiben
- Plugin verwenden
    - Auto Importer mit Plugin und im Developer Mode starten
    - Plugin sollte in der Plugin-Übersicht gelistet sein
    - Troubleshooting: aufzeigen, wo man sehen kann, wenn Plugin nicht geladen werden konnte
- Vorgehen ohne Template beschreiben
    - Manifest anlegen (Verweis auf Unterkapitel)
    - IPlugin implementieren (Verweis auf Plugin structure Kapitel)
- Beispiel als Zip zur Verfügung stellen
- Next steps
    - erwähnen, dass als nächstes Entscheidung bzgl. Modultyp notwendig ist
--->

This chapter describes what is required to create a minimal plug-in.

## manifest.json
```json
{
    "$schema": "../schemas/manifest.schema.json",
    "id": "Zeiss.ImportPluginDemo",
    "version": "1.0.0",
    "title": "ImportPlugin",
    "description": "Demonstrates the ability to add import sources to an import plan by implementing import modules.",
    "author": "Carl Zeiss",
    "contact": "info.metrology.de@zeiss.com",
    "homepage": "https://www.zeiss.com/metrology/en/software/zeiss-piweb.html",
    "issueTracker": "https://github.com/ZEISS-PiWeb/PiWeb-Import-Sdk/issues",
    "licenseName": "BSD-3-Clause",
    "licenseUrl": "https://github.com/ZEISS-PiWeb/PiWeb-Import-Sdk/blob/develop/LICENSE.txt",
    "sourceCode": "https://github.com/ZEISS-PiWeb/PiWeb-Import-Sdk",
    "documentation": "https://zeiss-piweb.github.io/PiWeb-Import-Sdk",
    "provides": [
        {
            "type": "ImportAutomation",
            "id": "MyImportModule",
            "title": "Custom Import Source",
            "description": "This import module does nothing and is only useful for testing import modules."
        }
    ]
}
```
The manifest describes your plugin content and defines the modules which are provided.\
**manifest.json** has to be in the root of your plugin folder.

### Plugin information
**$schema:** The schema we provide to validate your manifest.\
**id:** The unique identifier for this plugin. The plugin identifier must be unique overall installed plugins.\
**version:** Your managed version of the plugin.\
**title:** Title shown in plugin management view of Auto Importer.\
**description:** Description shown in plugin management view of Auto Importer.\
**provides:** Collection of module information.

![Plugin manifest](../../assets/images/getting_started/2_manifest.png "Plugin manifest")

### Module information
**type:** ImportAutomation or ImportFormat, see chapter [Choose your module type]({% link docs/getting_started/3_module_type.md %}) for further information.\
**id:** Unique identifer for this module, must be unique within the plugin.\
**title:** Title shown in the module selection of plugin management view of Auto Importer.\
**description:** Description shown in the module selection of plugin management view of Auto Importer.

## Import SDK nuget
The Import SDK nuget is required for the development of Auto Importer plugins. This can be obtained from TODO.\
> [!IMPORTANT]
> Please ensure that this assembly is not copied to the output.

```json
<PackageReference Include="Zeiss.PiWeb.Import.Sdk" Version="$(ImportSdkNuGetVersion)">
    <Private>false</Private>
    <ExcludeAssets>runtime</ExcludeAssets>
</PackageReference>
```
<!-- URL bereitstellen, in offiziellem nuget Feed? -->

## IPlugin and IPluginContext
IPlugin represents a PiWeb Auto Importer plugin. Here you register your modules with the Auto Importer.

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
> [!NOTE]
> In this example an ImportAutomation is registered. You can also register ImportFormat, read the next chapter [Choose your module type]({% link docs/getting_started/3_module_type.md %}) for more information.

**Init:** Initializes the plugin. Usually called during startup of the hosting application while showing a splash screen. Startup finishes when the returned task is completed. *RegisterImportAutomation* on *IPluginContext* is used to register your defined module with the system.

> [!IMPORTANT]
> The *id* of *RegisterImportAutomation* and *RegisterImportFormat* from *IPluginContext* needs to be identical with your manifest.

For an overview of the different module types, please follow the next chapter [Choose your module type]({% link docs/getting_started/3_module_type.md %}).