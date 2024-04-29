---
layout: default
nav_order: 1
parent: Plugin fundamentals
title: Plugin structure
---

# {{ page.title }}

<!---
Ziele:
- allgemeinen Aufbau eines Plugins beschreiben

Inhalt:
- Grundstruktur
    - Ordner mit Manifest und Assemblies
- wo sucht AI nach Plugins
- Manifest referenziert das Einstiegs-Assembly (direkt oder indirekt)
- Einstiegs-Assembly muss Implementierung von IPlugin haben
- Manifest-Übersetzung in Unterordnern erwähnen
- C#-Projektdatei beschreiben
    - SDK-NuGet referenzieren
    - WICHTIG: Referenz auf SDK-NuGet braucht Flag, um nicht in die Ausgabe kopiert zu werden
    - Flag in Projekteigenschaften, um zu markieren, dass es ein C#-Plugin ist (damit alle Abhängigkeiten in die Ausgabe gelegt werden)
--->
