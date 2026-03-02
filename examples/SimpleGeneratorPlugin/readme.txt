This example demonstrates a very basic import automation plug-in. The plug-in provides an import automation that can
be selected as an import source called "Simple Measurement Generator" in an import plan. When the import plan is
run, the automation will create new measurements with a random measured value in an interval of 5 seconds.

Since the manifest does not explicitly specify a main assembly, the main assembly of this plug-in must be named like
the plug-in id given in the manifest. In this case, this will be "SimpleGeneratorPlugin.dll" which is the main assembly
of this plug-in.

At application start the hosting application will load the main assembly and then it will try to find a class
implementing IPlugin in this assembly. If such a class is found, it will be instantiated using its default constructor
(which it must have). We implement the IPlugin interface in Plugin.cs. This makes it the entry point of the plug-in.
