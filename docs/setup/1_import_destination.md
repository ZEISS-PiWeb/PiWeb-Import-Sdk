---
layout: default
nav_order: 1
parent: Setup
title: Import destination
---

# {{ page.title }}

The import destination represents the storage location for the quality data handled by the developed plug-in. During development, we recommend using [PiWeb Cloud](https://piwebcloud.metrology.zeiss.com){:target="_blank"} to get a fully managed PiWeb Server. However, development is also possible with a new or existing PiWeb Server instance.

## PiWeb Cloud
<!-- TODO Anleitung? wie detailiert? -->
PiWeb Cloud is the new, cloud-based version of PiWeb in which the PiWeb Server and the PiWeb database are outsourced to the cloud hosted by Microsoft Azure. PiWeb Cloud may be used within a one-year subscription. Within this PiWeb Cloud subscription, you can adjust the number of licenses for the PiWeb applications and the size of the database according to your needs.

[PiWeb Cloud FAQ](https://piwebcloud.metrology.zeiss.com/faq-en){:target="_blank"}

[Try PiWeb for free over a period of 90 days.](https://piwebcloud.metrology.zeiss.com/){:target="_blank"}

## PiWeb Server
The PiWeb server establishes a connection from the applications PiWeb Monitor, PiWeb Designer, PiWeb Planner, PiWeb Notifications, PiWeb Inline Correlation and PiWeb Auto Importer to the databases. PiWeb supports the databases PiWeb sbs and Microsoft SQL Server. The PiWeb server can be installed on the same or on any other PC within the network. The server is configured in PiWeb Server.

If a configured PiWeb server already exists, that configuration will be displayed in the main window. You can edit configurations, add new configurations or delete configurations.

The installation and configuration can be found in the [PiWeb Server tech guide](https://techguide.zeiss.com/en/zeiss-piweb-2025r1/article/introduction_to_piweb_server.html){:target="_blank"}.

A corresponding license is required to operate a PiWeb Server.\
[Read more about PiWeb software.](https://www.zeiss.com/metrology/en/software/zeiss-piweb.html){:target="_blank"}

{% assign parent = page.parent %}
{% assign sorted_pages = site.pages | sort:"nav_order" %}
{% assign current_index = sorted_pages | index: page %}
{% assign next_page = nil %}

{% for p in sorted_pages %}
  {% if p.parent == parent and p.nav_order > page.nav_order %}
    {% assign next_page = p %}
    {% break %}
  {% endif %}
{% endfor %}

{% if next_page %}
[Weiter]({{ next_page.url }})
{% endif %}