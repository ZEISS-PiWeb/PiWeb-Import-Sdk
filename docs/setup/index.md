---
has_children: true
layout: default
nav_order: 2
has_toc: false
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
- The first requirement is the development environment which is necessary to compile your plug-ins. In [Development environment]({% link docs/setup/development_environment.md %}) we will show you how to setup this environment.
- The second requirement is a hosting application to run your plug-ins. This hosting application is usually PiWeb Auto Importer. However, PiWeb Auto Importer in turn requires a working backend to import data to. [PiWeb backend]({% link docs/setup/piweb_backend.md %}) shows the two options to get a working backend. In [PiWeb Auto Importer]({% link docs/setup/piweb_auto_importer.md %}) we will show you how to setup PiWeb Auto Importer to use this backend.

Since the documentation is focused on developing import plug-ins, we will only roughly cover the setup of the PiWeb backend and the Auto Importer.