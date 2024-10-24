---
title: Introduction
layout: home
nav_order: 1
---

<!---
Aim:
- Give a short explanation of what the context of this SDK
  - What is PiWeb
  - What is the Auto Importer, why is it needed
  - What is the import sdk used for
  - Why do we need plug-ins
- Explain what this documentation aims to do
- Explain the basic structure of the documentation 
--->

# Introduction

[ZEISS PiWeb software](https://www.zeiss.de/messtechnik/produkte/software/piweb.html){:target="_blank"} is a set of applications to store, manage and evaluate measurement data usually used for quality assurance in industrial production and other fields. While the PiWeb backend stores data in a structured way and makes it available to the network, PiWeb clients provide statistical and graphical evaluation and reporting. Data to be stored in PiWeb often comes directly from measuring machines producing output files of various formats containing measurement values. To always have current measurement data available, the import of these files needs to be automated.

PiWeb Auto Importer (as part of ZEISS PiWeb software) is an application that allows to automate the import of data files by watching one or more filesystem folders and automatically uploading the measurements of any file that appears in these folders. 

![Measuring flow](assets/images/measuring_flow.png "Measuring flow"){: .framed }

Although PiWeb Auto Importer understands most of the common formats typically output by measuring machines, support for customer specific file formats or entirely different data sources like REST services is often required. For this reason PiWeb Auto Importer can be extended via custom plug-ins. The PiWeb Import SDK is a framework that is used to write such custom plug-ins. As this framework is a .NET assembly, the development of import plug-ins is possible in any .NET language. However,  we will be using C# examples and setups throughout this documentation.

In the remaining sections we will give you the necessary tools to write your own PiWeb Auto Importer plug-ins. We will also explain more about the inner concepts and workings of such plug-ins and the infrastructure provided by PiWeb Auto Importer for configuring imports, running windows services and monitoring the configured imports.

In the next section [Setup]({% link docs/setup/index.md %}) we will show you everything necessary for getting your development environment ready to write plug-ins. In [Getting Started]({% link docs/getting_started/index.md %}) we will write and run a first very simple plug-in. After that, [Plug-in Fundamentals]({% link docs/plugin_fundamentals/index.md %}) covers more detailed explanations for the most important aspects of writing plug-ins. The following section [Advanced Topics]({% link docs/advanced_topics/index.md %}) focuses on making plug-ins more customizable by explaining advanced topics like user configuration and localization.

{: .note }
This document is still a draft. Some content is still missing and existing content needs a final revision.