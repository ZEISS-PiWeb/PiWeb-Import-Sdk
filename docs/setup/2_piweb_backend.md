---
layout: default
nav_order: 2
parent: Setup
title: PiWeb backend
---

# {{ page.title }}
The *PiWeb* backend is where *PiWeb* stores all measurement data. This backend provides a webservice that can be accessed via an open REST API. For more information see the corresponding [API documentation](http://zeiss-piweb.github.io/PiWeb-Api){:target="_blank"}. Since  *PiWeb Auto Importer* always imports new data to such a backend, a working backend is required as import target to run and test any import plugins in *PiWeb Auto Importer*. There are two options for *PiWeb* backends: *PiWeb Cloud* and *PiWeb Enterprise Server*.

 <!-- During development, we recommend using [PiWeb Cloud](https://piwebcloud.metrology.zeiss.com){:target="_blank"} to get a fully managed PiWeb Server. However, development is also possible with a new or existing PiWeb Server instance. -->

## PiWeb Cloud
The *PiWeb Cloud* is a manageable fully cloud-based backend for *PiWeb* that can be used as import target. It also provides all important *PiWeb* desktop clients like the *PiWeb Auto Importer* on a subscription basis. Since a free 90 days trial subscription is available, this is the recommended way for starting import plugin development without a lot of set-up work.

[Try PiWeb for free over a period of 90 days.](https://piwebcloud.metrology.zeiss.com/){:target="_blank"}  
[PiWeb Cloud FAQ](https://piwebcloud.metrology.zeiss.com/faq-en){:target="_blank"}

## PiWeb Enterprise Server
*PiWeb Enterprise Server* is an on-premises installable *PiWeb* backend. It requires a running *Microsoft SQL Server*. Although the installation of *PiWeb Enterprise Server* is fairly easy, quite some effort is required to configure the connection to the SQL server, setup user authentication, configure user permissions, ... and many more aspects.

If a running *PiWeb Enterprise Server* is available, it can easily be used as import target; otherwise, we suggest to use *PiWeb Cloud* as backend for testing import plugins in *PiWeb Auto Importer*.

More details about the installation and configuration can be found in the [PiWeb Server Tech Guide](https://techguide.zeiss.com/en/zeiss-piweb-2025r1/article/introduction_to_piweb_server.html){:target="_blank"} (requires an account).