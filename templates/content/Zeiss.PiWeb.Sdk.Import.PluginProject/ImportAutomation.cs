using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.PiWeb.Sdk.Import.PluginProject;

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
