---
layout: default
nav_order: 6
title: Deployment
---

# {{ page.title }}

<!---
Ziele:
- aufzeigen, wie das Plug-in beim Kunden eingerichtet werden kann

Inhalt:
- Installation
    - Ablage in Plug-in-Ordner
    - Installation via Auto Importer
        - eventuell Dateiendung beachten
- aktuell noch kein Plug-in-Store vorhanden
--->

The compiled plug-in must be packed together with the manifest file in a **pip** file. This pip file can be installed directly in the Auto Importer as a plug-in.\
This works via the file system, install function in the Auto Importer and via drag & drop into the Auto Importer window.

## File system
The Auto Importer goes through subfolders of the **"Plugins"** folder in its installation path to find a manifest.json there. It evaluates this manifest file and loads the corresponding plug-in when the program is started.\
This is also the only place in the production environment from which plug-ins are loaded. If you place a corresponding folder in this path, the Auto Importer will load it as a plug-in the next time the program is started.

## Install option
The Auto Importer provides an installer option, for which the following steps must be carried out:

1. Open **File** > **Plug-ins...**, You can select the Install plug-in... option via the cogwheel in the top right-hand area. option. Alternatively, if you have not yet installed a plug-in, you can select Install plug-in directly in the middle of the view.
![Install plug-in](../assets/images/deployment/ai_install_1.png "Install plug-in")\
This will open the file explorer with filtering for Auto Importer plug-in files.
2. After selecting the desired plug-in, an overview of the manifest data appears.
![Install window](../assets/images/deployment/ai_install_2.png "Install window")\
Administration rights are requested for installation.
3. After successful execution, you will be prompted to restart the Auto Importer.
![Restart request](../assets/images/deployment/ai_install_3.png "Restart request")

## Drag & drop
It is also possible to drag Auto Importer plug-in files into the Auto Importer window. This then automatically recognizes that an installation is desired. The overview window with the plug-in to be installed then appears directly.
Here too, the Auto Importer must be restarted afterwards.

This function allows you to install plug-ins directly from a mail attachment, for example.