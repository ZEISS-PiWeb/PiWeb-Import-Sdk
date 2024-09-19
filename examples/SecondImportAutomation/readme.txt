This example demonstrates a somewhat more complex import plug-in. The plug-in provides an import automation that can be
selected as import source in an import plan of the PiWeb Auto Importer. Running this automation fetches current weather
data in a given interval from a public whether data API and uploads these data to the PiWeb backend configured in the
import plan. The plug-in demonstrates how to integrate the PiWeb API into an import automation and how to provide custom
configuration items for the import plan.

Since the manifest does not explicitly specify a main assembly, the main assembly of this plug-in must be named like
the plug-in id given in the manifest. In this case, this will be "Zeiss.SecondImportAutomation.dll" which is the sole
assembly of this plug-in.

At application start the hosting application will load the main assembly and then it will try to find a class
implementing IPlugin in this assembly. If such a class is found, it will be instantiated using its default constructor
(which it must have). We implement the IPlugin interface in Plugin.cs. This makes it the entry point of the plug-in.
