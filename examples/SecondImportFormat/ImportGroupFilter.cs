#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;

namespace Zeiss.SecondImportFormat;

public class ImportGroupFilter : IImportGroupFilter
{
    public async ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context)
    {
        // Check file extension.
        if (!importGroup.PrimaryFile.HasExtension(".txt"))
            return FilterResult.None;

        // Add additional data to import group.
        var additionalData = context.CurrentImportFolder.FindFile(importGroup.PrimaryFile.BaseName + ".png");
        if (additionalData == null)
            return FilterResult.RetryOrImport;
        importGroup.AddFile(additionalData);

        return FilterResult.Import;
    }
}
