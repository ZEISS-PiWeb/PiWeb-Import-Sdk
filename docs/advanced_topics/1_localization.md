---
layout: default
nav_order: 1
parent: Advanced topics
title: Localization
---

# {{ page.title }}

<!---
Ziele:
- Mechanismus für Lokalisierung beschreiben

Inhalt:
- Übersetzung der Informationen im Manifest
- allgemeine Herangehensweise zur Lokalisierung von UI-Elementen beschreiben
--->

## Manifest
The plug-in system supports localization of the manifest file, in which the supported language abbreviations appear as additional subfolders in the "locals" subfolder and contain a manifest.json.\
![Localization](../../assets/images/plugin_fundamentals/1_localization.png "Localization")

Example content of a localized manifest.json insides locales\en-US\ folder:
```json
{
  "$schema": "../../../schemas/localizations.schema.json",
  "translation": {
    "title": "English title from plugin",
    "ImportAutomation18626.title": "Automation (localization) (Localized for [en-US])"
  }
}
```
Name value pairs are used.
<!-- Besseres Bsp.? -->

## Code
