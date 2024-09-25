#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.SecondImportAutomation;

public class ImportAutomation : IImportAutomation
{
    public IImportRunner CreateImportRunner(ICreateImportRunnerContext context)
    {
        // Creation of a new instance of IImportRunner, called for every import plan
        return new ImportRunner(context);
    }

    public IAutomationConfiguration CreateConfiguration(ICreateAutomationConfigurationContext context)
    {
        // Creation of a new instance of IAutomationConfiguration, called for every import plan
        return new AutomationConfiguration(context.PropertyStorage);
    }
}
