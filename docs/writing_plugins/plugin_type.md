---
layout: default
nav_order: 1
parent:  Writing plug-ins
title: Choose a plug-in type
---

<!---
Ziele:
- erste Entscheidungshilfe für Modultypen geben
    
Inhalt:
- Unterschiede von Modultypen erwähnen
- Entscheidungsgrundlage geben ("wenn du das machen willst, dann wähle Modultyp..., wenn du das machen willst dann...")
- für genauere Beschreibung der Anwendungsfälle und des technischen Ansatzes auf Kapitel in "Plug-in fundamentals" verweisen
- ggf. dieses Kapitel in vorheriges Kapitel (Create your first plug-in) als letzten Absatz integrieren
--->

# {{ page.title }}
*PiWeb* offers multiple types of import plug-ins, all of which are written using the *PiWeb Import SDK*. The main difference between these import plug-in types lies in which part of the built-in import process is replaced or extended by the plug-in. Currently there are two types of import plug-ins available:
- *Import format plug-ins* and
- *Import automation plug-ins*

Before we can start to write our first plug-in, you must choose which one of those two plug-in types you want to create.

## Import format plug-ins
*Import format plug-ins* add a new file format to the built-in file based import automation of *PiWeb Auto Importer*. The plug-in implementation can decide which files should be handled, how multiple files need to be grouped together (if this is necessary at all) and how to interpret the file contents as inspection plan and measurement data. Other aspects, such as watching for new import files, updating import logs and uploading data or running Windows services are still handled by *PiWeb Auto Importer* and cannot be customized with this plug-in type.  
Write *import format plug-ins* when you need to import files of a format that *PiWeb* does not support natively.

## Import automation plug-ins
*Import automation plug-ins* fully replace the built-in file based import automation with a custom import automation. Implementing such a custom import automation allows for complete freedom in fetching and writing import data. For example it is possible to implement an *import automation plug-in* that fetches data from sources like REST APIs or enterprise service buses (ESBs) instead of the file system. However, this also means that the usual *PiWeb Auto Importer* infrastructure of managing and watching the file system, import data post processing (attribute mapping and path rules) and even uploading inspection plan and measurement data is not available to these *import automation plug-ins*. Instead, creating or editing any data on the *PiWeb* backend is usually implemented by integrating and using the *PiWeb API* directly (although the hosting *PiWeb Auto Importer* will provide the access credentials configured in the import plan).  
Write *import automation plug-ins* when you need to import data from other sources than the file system or when other restrictions prevent you from using *import format plug-ins*. Avoid writing *import automation plug-ins* when the task is solvable by writing an *import format plug-in* as these plug-ins are far easier to implement.

## How to continue?
Based on your choice, we can now walk through creating a plug-in by following the steps in either [Writing an import format plug-in]({% link docs/writing_plugins/import_format.md %}) or [Writing an import automation plug-in]({% link docs/writing_plugins/import_automation.md %}).

{: .note }
If you do not have a concrete use case yet and only want to explore the plug-in feature of *PiWeb Auto Importer*, we generally recommend to start with an import format plug-in.