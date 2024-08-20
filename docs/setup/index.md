---
has_children: true
layout: default
nav_order: 2
title: Setup
---

<!---
Ziele:
- aufzeigen, was benötigt und wie eingerichtet wird, um ein Plug-in entwickeln zu können
 
Inhalt:
- Nutzung von Visual Studio / Rider / VS Code erwähnen
- Installation und Einrichtung von PiWeb
    - PiWeb Lizenz als Voraussetzung (PiWeb Cloud als Alternative)
    - PiWeb Server muss laufen
- Einrichtung des Auto Importers
    - Importplan anlegen, Zieldatenbank definieren, Importquelle festlegen
    - Aktivierung des Developer Mode für Auto Importer beschreiben
    - Verwendung Kommandozeilenparameter für Plug-in-Ordner erklären
- Template erwähnen, muss noch in GitHub bereitgestellt werden und auf Formats erweitert werden
--->

# {{ page.title }}

The following chapters define the environment required to start developing your own plug-in for the Auto Importer.

Chapter [Import destination]({% link docs/setup/1_import_target.md %}) provides an overview of the key components and functionalities related to the import destination in the PiWeb Auto Importer.\
[Auto Importer]({% link docs/setup/2_auto_importer.md %}) is a tool that enables automatic import of measurement data from various formats into the PiWeb database. It provides functionalities for configuring automated import settings and monitoring the status of import plans.

Chapter [Development settings]({% link docs/setup/3_development_settings.md %}) outlines the necessary settings for starting plug-in development, including activating the development mode and configuring external plug-in folders. Additionally, chapter [Development environment]({% link docs/setup/4_development_environment.md %}) provides recommendations for editors and required software versions for using the Import SDK, along with information about the project template and Import SDK nuget configuration.

Finally, the process of installing and using a compiled plug-in within the Auto Importer is described in chapter [Starting a plug-in]({% link docs/setup/5_starting_plugin.md %}), covering steps from downloading the plug-in to configuring import plans with custom modules.