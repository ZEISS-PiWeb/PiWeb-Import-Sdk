ZEISS PiWeb-Import-Sdk
=========

[![Build on develop](https://github.com/ZEISS-PiWeb/PiWeb-Import-Sdk/actions/workflows/develop.yml/badge.svg?branch=develop&event=push)](https://github.com/ZEISS-PiWeb/PiWeb-Import-Sdk/actions/workflows/develop.yml)
[![License](https://img.shields.io/badge/License-BSD%203--Clause-blue.svg)](https://opensource.org/licenses/BSD-3-Clause)
[![Nuget](https://img.shields.io/nuget/v/Zeiss.PiWeb.Sdk.Import?style=flat&logo=nuget)](https://www.nuget.org/packages/Zeiss.PiWeb.Sdk.Import/)

The PiWeb-Import-Sdk is the basic framework for creating your own import plug-ins for the PiWeb Auto Importer. By creating plug-ins, it is possible to automate the data import of your own custom file formats or even import data from other sources like REST-APIs or databases.

> [!NOTE]
> You can find our documentation for plug-in developers under [import plug-in documentation](https://zeiss-piweb.github.io/PiWeb-Import-Sdk/).

### What is the PiWeb Auto Importer?
The PiWeb Auto Importer is an application of the [ZEISS PiWeb software](https://www.zeiss.de/messtechnik/produkte/software/piweb.html). PiWeb applications enable the management, provision and evaluation of measurement data typically encountered in the field of industrial measurement technology.

The measurement data to manage, however, is provided by sources like measuring machines. To make such measurement data available for evaluation in PiWeb applications, it needs to be imported from its source into the PiWeb data backend. Most commonly measuring machines output their measurements as files in the file system. In this case, the Auto Importer can be used to automate the import and backup of such measurement files as soon as they appear. It provides a configuration UI and can be setup to run either as an application in the background or even fully automated as a Windows service.

### Why write an import plug-in?
While the Auto Importer has built-in support for most common file formats of measurement data, it does not know how to handle more customer specific file formats. plug-ins can be used to add seamless support for such custom formats while still leaving the tasks of file detection, file backup and windows service management to the Auto Importer.

Another use case for writing plug-ins is importing data from sources other than files such as rest services or databases.

### How to write an import plug-in?
Getting started is as easy as creating a new C# project in your favorite IDE, adding a plug-in manifest and linking the [Zeiss.PiWeb.Sdk.Import](https://www.nuget.org/packages/Zeiss.PiWeb.Sdk.Import/) NuGet. To make this even easier, we provide a project template you can use as a starting point. More detailed information about this can be found in our [import plug-in documentation](https://zeiss-piweb.github.io/PiWeb-Import-Sdk/).

### Version compatibility
The following table lists which versions of PiWeb will support plug-ins written against which version of the Import SDK. It also shows which .NET runtime will be used to run plug-ins.

| PiWeb version | Import SDK version | .NET version
| - | - | - |
| &ge; 2025.R1.* | 1.0 | net8.0-windows10.0.22621 |
| &le; 8.6.* | no plug-in support | no plug-in support |

### Learn more
* More information about the ZEISS PiWeb software can be found on the [PiWeb homepage](https://www.zeiss.de/messtechnik/produkte/software/piweb.html).
* Our [import plug-in documentation](https://zeiss-piweb.github.io/PiWeb-Import-Sdk/) explains how to write plug-ins.
* The [PiWeb domain model](https://zeiss-piweb.github.io/PiWeb-Api/general#gi-model) explains how data is structured in the backend. Import plug-ins create import data structured like this.
* The [PiWeb-API](https://github.com/ZEISS-PiWeb/PiWeb-Api) is an open source implementation of the REST-API provided by the data backend. It may be used within import plug-ins if direct access to the backend is required.

