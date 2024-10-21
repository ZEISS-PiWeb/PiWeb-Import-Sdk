---
layout: default
nav_order: 3
parent: Setup
title: PiWeb Auto Importer
---

# {{ page.title }}

Import plug-ins extend the built-in functionality of *PiWeb Auto Importer* either by implementing an additional type of import automation or by implementing an additional import format for the built-in file based import automation. 

A single instance of the *PiWeb Auto Importer* can run and control any number of import automations. Each import automation is configured by an import plan and can be run and controlled independently. In order to test an import plug-in, we need to do two things: Firstly, we need to make the plug-in known to the *PiWeb Auto Importer* and secondly, we need to create, configure and run a suitable import plan that uses the plug-in.

In this document we will show you how to generally setup and prepare the *PiWeb Auto Importer* for testing plug-ins. Later when developing our first plug-ins in [Gettings started]({% link docs/getting_started/index.md %}), we will also show you how to actually run these plug-ins using *PiWeb Auto Importer*. 

{: .note }
For more information about the different plug-in types and how they are used by *PiWeb Auto Importer*, see [Plug-in types]({% link docs/plugin_fundamentals/2_plugin_type.md %}).

## Plug-in search paths
Import plug-ins are usually installed via *.pip* files to a specific plug-in installation folder. However, since the plug-in installation is system wide, administrator privileges would be required for each change. At the customer side this is desired for security reasons, however, this is not practical for plug-in development at all. For this reason, *PiWeb Auto Importer* has a special development mode that allows to load plug-ins from custom unprivileged locations. To activate the development mode, you need to create a specific value in the windows registry:

In `HKEY_LOCAL_MACHINE\SOFTWARE\Zeiss\PiWeb\AutoImporter`, if it does not yet exist, create a new *string* value `DevelopmentMode` and set it to `true`.

![Editing development mode in Windows registry editor](../../assets/images/setup/3_regedit_development_mode.png "Editing development mode in Windows registry editor"){: .framed }

*PiWeb Auto Importer* will now show [Development mode] in its title bar.

![Active development mode in PiWeb Auto Importer](../../assets/images/setup/3_titlebar_development_mode.png "Active development mode in Auto Importer"){: .bare }

With active development mode, it is possible to specify additional plug-in search paths via command line parameter <span class="nowrap">`-pluginSearchPaths`</span> when starting *PiWeb Auto Importer*. The typical usage of this parameter is to set the plug-in search path directly to the output folder of your plug-in project.

![PiWeb Auto Importer command line example](../../assets/images/setup/3_cmd_plugin_search_path.png "PiWeb Auto Importer command line example"){: .bare }

## Plug-in management
To see whether plug-ins in custom search paths are found, you can compile one of the example plugins, use its build output path as custom search path and open the plugin management view.

![Plugins in the file menu](../../assets/images/setup/3_file_menu_plugins.png "Plugins in the file menu"){: .bare }

![Plugin management](../../assets/images/setup/3_plugin_management.png "Plugin management"){: .bare }

{: .note }
Plug-ins from custom search paths cannot be uninstalled like regular installed plug-ins.

## Import plans
The last step of setting up *PiWeb Auto Importer* is to create and configure an import plan we can run. A new import plan can easily be created by clicking the green plus icon in the toolbar. If there are no import plans created so far, you can use the displayed link to create the a first import plan instead. You can now enter a name for the newly created import plan or just keep the default name.

![Create new default import plan](../../assets/images/setup/3_create_default_import_plan.png "Create new default import plan"){: .bare }

Most of the default settings of the import plan are fine, however, it is still missing a valid import target. Use the "Select connection" button to choose an import target. The import target must be a running *PiWeb backend*, see [PiWeb backend]({% link docs/setup/2_piweb_backend.md %}) for your options to set one up.

![Plugin management](../../assets/images/setup/3_select_connection.png "Plugin management"){: .framed }

Depending on your chosen import target, you may also need to authenticate at the import target. When everything is working correctly, the connection status should now be 'Ready'.

This is all the basic setup you need. In the next section [Getting started]({% link docs/getting_started/index.md %}), we will show you how to create, build an run your own plug-ins.