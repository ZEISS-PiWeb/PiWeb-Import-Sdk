using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.ImportData;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

namespace Zeiss.PiWeb.Sdk.Import.PluginProject;

public class ImportParser : IImportParser
{
    public async Task<ImportData> ParseAsync(
        IImportGroup importGroup,
        IParseContext context,
        CancellationToken cancellationToken = default)
    {
        var root = new InspectionPlanPart("root");

        await Task.Delay(100, cancellationToken);

        return new ImportData(root);
    }
}
