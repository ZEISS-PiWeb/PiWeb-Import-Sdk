#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.StartingAPlugin;

public class ImportAutomation : IImportAutomation
{
    // An IImportAutomation instance usually is little more than a general factory for the different objects
    // required by the hosting application. While there will normally be only one instance of ImportAutomation during
    // application lifetime, multiple instances of IImportRunner and IAutomationConfiguration may be created and later
    // disposed.

    public IImportRunner CreateImportRunner(ICreateImportRunnerContext context)
    {
        // This method is called by the hosting application whenever an import plan (configured to use this automation)
        // is started. We simply create an instance of our own IImportRunner implementation.
        // This returned runner will be started by the hosting application shortly afterward.

        return new ImportRunner(context);
    }

    // We could implement CreateConfiguration() here to provide custom entries in the import plan configuration UI
    // but this plug-in does not need any custom configuration.
}
