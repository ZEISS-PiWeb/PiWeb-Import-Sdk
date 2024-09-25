#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Import;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.StartingAPlugin;

public class Plugin : IPlugin
{
    // This class is instantiated once when the hosting application loads its plug-ins. It must provide a default
    // constructor. Like in this implementation, the default constructor can be implicit.

    // Depending on the specified type in the 'provides' section of the manifest, either CreateImportAutomation()
    // or CreateImportFormat() must be implemented. In this case the manifest specifies 'ImportAutomation' as type.

    public IImportAutomation CreateImportAutomation(ICreateImportAutomationContext context)
    {
        // This method is called once when the hosting application is initializing its plug-ins. The purpose of this
        // method is only to create a plug-in specific instance of IImportAutomation which implements all the plug-in
        // logic.

        return new ImportAutomation();
    }
}
