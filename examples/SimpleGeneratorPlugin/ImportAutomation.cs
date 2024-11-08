#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace SimpleGeneratorPlugin;

public class ImportAutomation : IImportAutomation
{
    public IImportRunner CreateImportRunner(ICreateImportRunnerContext context)
    {
        return new ImportRunner(context);
    }

    public IAutomationConfiguration CreateConfiguration(ICreateAutomationConfigurationContext context)
    {
        return new AutomationConfiguration(context);
    }
}
