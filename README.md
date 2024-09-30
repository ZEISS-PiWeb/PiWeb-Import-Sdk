ZEISS PiWeb-Import-Sdk
=========

[![Build on develop](https://github.com/ZEISS-PiWeb/PiWeb-Import-Sdk/actions/workflows/develop.yml/badge.svg?branch=develop&event=push)](https://github.com/ZEISS-PiWeb/PiWeb-Import-Sdk/actions/workflows/develop.yml)
[![License](https://img.shields.io/badge/License-BSD%203--Clause-blue.svg)](https://opensource.org/licenses/BSD-3-Clause)
[![Nuget](https://img.shields.io/nuget/v/Zeiss.PiWeb.Import.Sdk?style=flat&logo=nuget)](https://www.nuget.org/packages/Zeiss.PiWeb.Import.Sdk/)

The PiWeb-Import-Sdk is the basic framework for creating your own import plugins for the PiWeb Auto Importer. By creating plugins, it is possible to automate the data import of your own custom file formats or even import data from other sources like REST-APIs or databases.

### What is the PiWeb Auto Importer?
The PiWeb Auto Importer is an application of the [ZEISS PiWeb software](https://www.zeiss.de/messtechnik/produkte/software/piweb.html). PiWeb applications enable the management, provision and evaluation of measurement data typically encountered in the field of industrial measurement technology.

The measurement data to manage, however, is provided by sources like measuring machines. To make such measurement data available for evaluation in PiWeb applications, it needs to be imported from its source into the PiWeb data backend. Most commonly measuring machines output their measurements as files in the file system. In this case, the Auto Importer can be used to automate the import and backup of such measurement files as soon as they appear. It provides a configuration UI and can be setup to run either as an application in the background or even fully automated as a Windows service.

### Why write an import plugin?
While the Auto Importer has built-in support for most common file formats of measurement data, it does not know how to handle more customer specific file formats. Plugins can be used to add seamless support for such custom formats while still leaving the tasks of file detection, file backup and windows service management to the Auto Importer.

Another use case for writing plugins is importing data from sources other than files such as rest services or databases.

### How to write an import plugin?
> [!NOTE]
> The initial version of the PiWeb-Import-Sdk is still in development. The linked import plugin documentation is currently only a skeleton without actual content.

Getting started is as easy as creating a new C# project in your favorite IDE, adding a plugin manifest and linking the [Zeiss.PiWeb.Import.Sdk](https://www.nuget.org/packages/Zeiss.PiWeb.Import.Sdk/) NuGet. To make this even easier, we provide a project template you can use as a starting point. More detailed information about this can be found in our [import plugin documentation](https://zeiss-piweb.github.io/PiWeb-Import-Sdk/).

### Learn more

* More information about the ZEISS PiWeb software can be found on the [PiWeb homepage](https://www.zeiss.de/messtechnik/produkte/software/piweb.html).
* Our [import plugin documentation](https://zeiss-piweb.github.io/PiWeb-Import-Sdk/) explains how to write plugins.
* The [PiWeb domain model](https://zeiss-piweb.github.io/PiWeb-Api/general#gi-model) explains how data is structured in the backend. Import plugins create import data structured like this.
* The [PiWeb-API](https://github.com/ZEISS-PiWeb/PiWeb-Api) is an open source implementation of the REST-API provided by the data backend. It may be used within import plugins if direct access to the backend is required.
