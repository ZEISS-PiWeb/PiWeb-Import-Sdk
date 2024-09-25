using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

namespace Zeiss.PiWeb.Sdk.Import.PluginProject;

public class ImportFormat : IImportFormat
{
    public IImportGroupFilter CreateImportGroupFilter(ICreateImportGroupFilterContext context)
    {
        return new FileExtensionImportGroupFilter(".xyz");
    }

    public IImportParser CreateImportParser(ICreateImportParserContext context)
    {
        return new ImportParser();
    }
}