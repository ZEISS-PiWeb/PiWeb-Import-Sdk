#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.IO;
using System.Text;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;

namespace SimpleTxtPlugin;

public class SimpleTxtImportGroupFilter : IImportGroupFilter
{
    public async ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context)
    {
        // Check file extension.
        if (!importGroup.PrimaryFile.HasExtension(".txt"))
            return FilterResult.None;

        await using var stream = importGroup.PrimaryFile.GetDataStream();
        using var reader = new StreamReader(stream, Encoding.UTF8);

        // Check file content match with SimpleTxt format.
        var firstLine = await reader.ReadLineAsync();
        if (firstLine != null && firstLine.StartsWith("#Header"))
            return FilterResult.Import;

        return FilterResult.None;
    }
}
