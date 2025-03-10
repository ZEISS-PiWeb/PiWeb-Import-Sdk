---
layout: default
nav_order: 4
title: Debugging plug-ins
---

# {{ page.title }}
When a plug-in does not behave as expected and the problem can be reproduced, debugging the implementation is usually the easiest way to find the problem. In this article we will show you how to debug your plug-ins just like any other application.

{: .note }
When debugging is not an available option for any reason, looking at a log file is usually a more accessable way of problem analysis. Details about how to write log entries from you plug-ins are available in our dedicated [Debug logging]({% link docs/advanced_topics/logging.md %}) article.

## Run and debug

An import plug-in can be debugged simply by attaching a debugger to an *PiWeb Auto Importer* process using the plug-in. Ideally a debug build of the plug-in. If you have used our project template to create the *.NET* project of the plug-in (see [Installing project templates]({% link docs/setup/development_environment.md %}#installing-project-templates)), the project already contains a launch profile to do this in a single click:

![Debug plug-in](../assets/images/debugging_plugins/debugging.png "Debug plug-in")

If you do not have this launch configuration, you can easily add it to your project by creating a `launchSettings.js` file in the `Properties` folder of your project:
```json
{
  "profiles": {
    "AutoImporter": {
      "commandName": "Executable",
      "workingDirectory": "$(ProjectDir)",
      "executablePath": "%ProgramFiles%\\Zeiss\\PiWeb\\AutoImporter.exe",
      "commandLineArgs": "-pluginSearchPaths $(OutDir)"
    }
  }
}
```

{: .note }
> For this to work correctly, two conditions need to be met:
> - *PiWeb Auto Importer* must be installed locally.  
 The executable is expected to be found in <span class="nowrap">`%ProgramFiles%\Zeiss\PiWeb\AutoImporter.exe`</span>. If the *PiWeb Auto Importer* executable is in another path, you can update the path specified in `launchSettings.json` accordingly.
> - *PiWeb Auto Importer* must be in development mode.  
 See [Development mode]({% link docs/setup/piweb_auto_importer.md %}#development-mode) for details on how to activate development mode.