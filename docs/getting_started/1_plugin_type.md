---
layout: default
nav_order: 1
parent: Getting started
title: Choose your plug-in type
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
The PiWeb Import SDK provides two different types of plug-ins: **Format** and **Automation**. These plug-ins are designed to allow customized data import to PiWeb.

## Format plug-ins
Format plug-ins, on the other hand, are designed to support different data formats. They are used when you need to import data in a file format that PiWeb does not support natively.\
Read [Create your first import format]({% link docs/getting_started/2_import_format.md %}) to start with your first Format.

## Automation plug-ins
Automation plug-ins are designed to automate the process of data import from various sources. They are ideally used when you need to import data regularly from a source that PiWeb does not support by default. E.g. from a REST api.\
Read [Create your first import automation]({% link docs/getting_started/3_import_automation.md %}) to start with your first Automation.

## Choice
The choice of plug-in type depends on the specific use case:

- If you need to import data from a non-file-based source, then an **Automation plug-in** is the ideal choice.
- If you need to import data from a file-based format, then a **Format plug-in** would be the best choice.

For more detailed information on the application cases and the technical approach, refer to the plug-in fundamentals [Plug-in type]({% link docs/plugin_fundamentals/2_plugin_type.md %}).