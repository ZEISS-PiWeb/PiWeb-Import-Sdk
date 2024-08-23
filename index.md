---
title: Home
layout: home
nav_order: 1
---

# Welcome to PiWeb Import SDK documentation
![PiWeb hero image](assets/images/zeiss_piweb_heroimage.jpg "PiWeb")

Welcome to our Import SDK documentation. This handbook is designed to walk you through the essential steps to quickly set up your development environment and get started with building plug-ins, choosing plug-in types, creating import formats and import automations.

The PiWeb-Import-Sdk is the basic framework for creating your own import plug-ins for the PiWeb Auto Importer. By creating plug-ins, it is possible to automate the data import of your own custom file formats or even import data from other sources like REST-APIs or databases.

### What is the PiWeb Auto Importer?
The PiWeb Auto Importer is an application of the [ZEISS PiWeb software](https://www.zeiss.de/messtechnik/produkte/software/piweb.html){:target="_blank"}. PiWeb applications enable the management, provision and evaluation of measurement data typically encountered in the field of industrial measurement technology.

The measurement data to manage, however, is provided by sources like measuring machines. To make such measurement data available for evaluation in PiWeb applications, it needs to be imported from its source into the PiWeb data backend. Most commonly measuring machines output their measurements as files in the file system. In this case, the Auto Importer can be used to automate the import and backup of such measurement files as soon as they appear. It provides a configuration UI and can be setup to run either as an application in the background or even fully automated as a Windows service.

### Why write an import plug-in?
While the Auto Importer has built-in support for most common file formats of measurement data, it does not know how to handle more customer specific file formats. Plug-ins can be used to add seamless support for such custom formats while still leaving the tasks of file detection, file backup and windows service management to the Auto Importer.

Another use case for writing plug-ins is importing data from sources other than files such as rest services or databases.

Setup your needed environment: [Setup]({% link docs/setup/1_import_target.md %}).