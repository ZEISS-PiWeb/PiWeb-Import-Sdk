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
    public IImportAutomation CreateImportAutomation(ICreateImportAutomationContext context)
    {
        return new ImportAutomation();
    }
}
