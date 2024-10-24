---
has_children: true
layout: default
nav_order: 3
has_toc: false
title: Getting started
---

# {{ page.title }}
After going through the basic setup of our development environment in the last section, we are now ready to write our first fully functional import plug-in. In this section, we will show you how to create and implement a reasonably simple import plug-in and how to run this plug-in in the *PiWeb Auto Importer*.

{: .note }
The result of this guide will be a very good starting point for adding your own custom import logic.

As a first step you need to decide which type of import plug-in you want to write. In the article [Choose a plug-in type]({% link docs/getting_started/plugin_type.md %}) we will briefly introduce the two types of import plug-ins and talk about which one of these is better suited for your use case. 

Depending on your choice we will then resume with creating a first plug-in in either [Create your first import format]({% link docs/getting_started/import_format.md %}) or [Create your first import automation]({% link docs/getting_started/import_automation.md %}).