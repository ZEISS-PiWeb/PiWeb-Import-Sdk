#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System.IO;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;

#endregion

namespace Zeiss.FirstImportFormat
{
    public class SimpleTxtImportGroupFilter : IImportGroupFilter
    {
        public async ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context)
        {
            // Check file extension.
            if (!importGroup.PrimaryFile.HasExtension(".txt"))
                return FilterResult.None;

            await using var stream = importGroup.PrimaryFile.GetDataStream();
            using var reader = new StreamReader(stream);

            // Check file content match with SimpleTxt format.
            var firstLine = reader.ReadLine();
            if (firstLine != null && firstLine.StartsWith("#Header"))
                return FilterResult.Import;

            return FilterResult.None;
        }
    }
}
