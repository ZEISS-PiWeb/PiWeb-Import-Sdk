---
layout: default
nav_order: 1
parent: Setup
title: Development environment
---

# {{ page.title }}

## .NET SDK
To develop import plug-ins, you need a working *.NET* development environment. At minimum this requires installing the *.NET SDK* ([download](https://dotnet.microsoft.com/en-us/download){:target="_blank"}).

Some IDEs like *Visual Studio* may already take care of installing the *.NET SDK* for you. *Visual Studio* for example will install the *.NET SDK* when the workload *.NET desktop development* is checked during installation.

![Visual Studio workload](../../assets/images/setup/1_vs_workload.png "Visual Studio workload"){: .framed }

## Project templates
After installing the *.NET SDK* you can use can use your favorite IDE to develop import plug-ins. For the two most common IDEs we also provide project templates that make it easy to create new plug-in projects by setting up the correct project structure (including a basic manifest file) and adding the *PiWeb Import SDK* NuGet package to the new project.

### Visual Studio
.NET provides command line tooling for installing project templates from NuGet. Open a terminal and run `dotnet new install {package-id}`. Visual Studio will now show a template called `PiWeb AutoImporter Plug-in` when creating a new project.

{: .important}
**TODO:** Provide a project template on NuGet and update the package id.

### Rider
Download and unpack the project template ([download](https://){:target="_blank"}) to a folder of your choice. In the `New Project` or  `New Solution` dialog, click `Install templates...` on the bottom left. Select the folder containing the project template. Rider will now show a template called `PiWeb AutoImporter Plug-in` when creating a new project.

{: .important}
**TODO:** Provide a project template to download and update the link.

{: .important }
Rider < 2024.1 does not support entering values for optional parameters for custom project templates.

<!-- ## Import SDK nuget
The Import SDK nuget is required for the development of Auto Importer plug-ins. This can be obtained from .

{: .important }
> Please ensure that this assembly is not copied to the output:
> ```xml
> <PackageReference Include="Zeiss.PiWeb.Sdk.Import" Version="$(ImportSdkNuGetVersion)">
>    <Private>false</Private>
>     <ExcludeAssets>runtime</ExcludeAssets>
> </PackageReference>
> ``` -->
<!-- ImportSdkNuGetVersion immer defined? -->