---
layout: default
nav_order: 5
title: Deploying plug-ins
---

<!---
Ziele:
- aufzeigen, wie das Plug-in beim Kunden eingerichtet werden kann

Inhalt:
- Installation
    - Ablage in Plug-in-Ordner
    - Installation via Auto Importer
        - eventuell Dateiendung beachten
- aktuell noch kein Plug-in-Store vorhanden
--->

# {{ page.title }}
{: .no_toc }
So far, we used to run, debug and test plug-ins by loading them in *PiWeb Auto Importer* directly from their build output directory via command line parameter <span class="nowrap">`-pluginSearchPaths`</span>. While easy for testing and debugging, this is, of course, not a robust way to deploy a plug-in in a production environment. In this article we will show you how to create an easily distributable plug-in package and how to install it in a *PiWeb Auto Importer* not running in development mode.

## Table of Contents
{: .no_toc }
1. TOC
{:toc}

## Creating the plug-in package
A *PiWeb Installable Plug-in* (.pip) package file is automatically created on every build of your plug-in project as long as the *PiWeb Import SDK* NuGet package is referenced by the project. The created .pip file is written to `$(PackageOutputPath)` which usually is either `bin\Debug` or `bin\Release`, depending on your build configuration. A .pip file contains the complete plug-in implementation and does not have any other dependencies. As it is a single file, it is easy to distribute.

{: .note}
>You can switch building of the package file off by setting the `GeneratePluginPackageOnBuild` property in the project file to `false`:
>```xml
><GeneratePluginPackageOnBuild>false</GeneratePluginPackageOnBuild>
>```

## Distributing the plug-in package
Unfortunately, there is currently no package management infrastructure available that could be used to manage, share and distribute packages centrally. You can, however, manage and distribute packages by your own means.

## Installing the plug-in package
Plug-in packages can directly be installed in *PiWeb Auto Importer* in two different ways:
1. by dropping the package file to install anywhere in *PiWeb Auto Importer*, or
1. by opening the plug-in management view via `File > Plug-ins...` and then clicking on `Install plug-in...` in the management actions dropdown. You will then be prompted to choose the package file to install.
   
   ![Install plug-in](../assets/images/deployment/ai_install_1.png "Install plug-in")

*PiWeb Auto Importer* will show an overview of the plug-in to install. You can continue by pressing the `Install` button.

![Install window](../assets/images/deployment/ai_install_2.png "Install window")

Now the new plug-in is listed in the plug-in management view. However, it is not active yet since a restart is required to actually install and load it. At this point you can continue to install or remove further plug-ins before finally pressing the <span class="nowrap">`Restart now`</span> button in the info belt at the bottom of the window.

![Restart request](../assets/images/deployment/ai_install_3.png "Restart request")

Before *PiWeb Auto Importer* starts again, the plug-in installer opens. It displays all pending installation operations. You can press the `Run now` button to apply all these changes. 

![Plug-in Installer](../assets/images/deployment/ai_install_4.png "Plug-in Installer")

At this point the installer prompts for administrative privileges (only if you do not already have them), stops all import plans running as windows services and applies the plug-in changes. Afterwards, all previously stopped windows services will be started again. After closing the installer, *PiWeb Auto Importer* starts normally.

{: .note}
Pressing the `Discard` button closes the installer, drops all scheduled plug-in operations and starts *PiWeb Auto Importer* without changing the plug-in setup. Pressing the `Later` button instead will also close the installer and start *PiWeb Auto Importer*. However, the scheduled plug-in operations are not discarded. You will be prompted again to apply them after the next restart.
