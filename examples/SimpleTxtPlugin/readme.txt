This example demonstrates a very basic import format plug-in. The plug-in provides an import format called "SimpleTxt"
that is added to the list of known file formats when using the built-in file based import automation of PiWeb Auto
Importer. An example file of the implemented format can be found in the SampleData folder. This format is used in the
getting started guide for import format plug-ins:
https://zeiss-piweb.github.io/PiWeb-Import-Sdk/docs/getting_started/import_format.html

Since the manifest does not explicitly specify a main assembly, the main assembly of this plug-in must be named like
the plug-in id given in the manifest. In this case, this will be "SimpleTxtPlugin.dll" which is the sole assembly
of this plug-in.

At application start the hosting application will load the main assembly and then it will try to find a class
implementing IPlugin in this assembly. If such a class is found, it will be instantiated using its default constructor
(which it must have). We implement the IPlugin interface in Plugin.cs. This makes it the entry point of the plug-in.
