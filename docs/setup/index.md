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
Before we can start using the PiWeb Import SDK to develop import plug-ins, we need a working environment for compiling and running these plug-ins. This section explains how to setup such an environment.

In principle, there are only two requirements we need to cover:
- The first requirement is the development environment which is necessary to compile your plug-ins. In [Development environment]({% link docs/setup/1_development_environment.md %}) we will show you how to setup this environment.
- The second requirement is the hosting application to run your plug-ins. This hosting application is usually the PiWeb Auto Importer. However, the PiWeb Auto Importer in turn requires a working backend to import data to.

Since the documentation is focused on developing import plug-ins, we will only roughly cover the setup of the PiWeb backend and the Auto Importer.

<!-- [Import destination]({% link docs/setup/1_import_target.md %}) provides an overview of the key components and functionalities related to the import destination in the PiWeb Auto Importer.\
[Auto Importer]({% link docs/setup/2_auto_importer.md %}) is a tool that enables automatic import of measurement data from various formats into the PiWeb database. It provides functionalities for configuring automated import settings and monitoring the status of import plans.

[Development settings]({% link docs/setup/3_development_settings.md %}) outlines the necessary settings for starting plug-in development, including activating the development mode and configuring external plug-in folders. Additionally, [Development environment]({% link docs/setup/1_development_environment.md %}) provides recommendations for editors and required software versions for using the Import SDK, along with information about the project template and Import SDK nuget configuration.

Finally, the process of installing and using a compiled plug-in within the Auto Importer is described in [Starting a plug-in]({% link docs/setup/5_starting_plugin.md %}), covering steps from downloading the plug-in to configuring import plans with custom import source. -->