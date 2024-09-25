using Zeiss.PiWeb.Sdk.Import;
#if (UseAutomationModule)
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;
#endif
#if (UseFormatModule)
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;
#endif

namespace Zeiss.PiWeb.Sdk.Import.PluginProject;

public class Plugin : IPlugin
{
#if (UseAutomationModule)
    public IImportAutomation CreateImportAutomation(ICreateImportAutomationContext context)
    {
        return new ImportAutomation();
    }
#endif
#if (UseFormatModule)
    public IImportFormat CreateImportFormat(ICreateImportFormatContext context)
    {
        return new ImportFormat();
    }
#endif
}