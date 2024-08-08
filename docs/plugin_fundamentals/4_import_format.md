---
layout: default
nav_order: 4
parent: Plug-in fundamentals
title: Module Import format
---

# {{ page.title }}

<!---
Ziele:
- Hinweise zur Implementierung des Modultyps geben (insbesondere Dateigruppierung, Parsen, Prüfplan bauen)

Inhalt:
- Implementierung von IImportFormat beschreiben
- File grouping
    - Beschreibung der GetGroup-Methode
    - insbesondere Status-Enum erklären
- Parsing & building inspection plan
    - Möglichkeiten zum Lesen der Datei 
    - Erstellung eines TreeDataImage erklären
        - welche Entitäten können belegt werden
        - AttributeTemplates erklären
--->

```c#
public enum ImportAction
{
    Pass,           // ImportGroup should not be imported by this module (wrong file/format)
    Import,         // ImportGroup is to be imported, all necessary data is available
    Discard,        // ImportGroup is invalid, the files should be discarded directly
    RetryOrImport,  // ImportGroup is to be imported, but we are still waiting for possible additional files, e.g. additional data
    RetryOrDiscard, // ImportGroup should not be imported, but there may still be files that make importing possible
}
```