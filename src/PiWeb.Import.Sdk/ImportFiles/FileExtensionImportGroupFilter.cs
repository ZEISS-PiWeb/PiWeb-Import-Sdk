#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeiss.PiWeb.Import.Sdk.ImportFiles;

/// <summary>
/// <inheritdoc />
/// This implementation filters import groups by the file extension of the primary import file. A group with any of the
/// predefined file extensions will be imported. All other groups are passed further down the filtering chain. 
/// </summary>
public class FileExtensionImportGroupFilter : IImportGroupFilter
{
    private readonly string[] _AcceptedExtensions;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileExtensionImportGroupFilter"/> class. 
    /// </summary>
    /// <param name="acceptedExtensions">A list of all accepted file extensions.</param>
    public FileExtensionImportGroupFilter(IEnumerable<string> acceptedExtensions)
    {
        _AcceptedExtensions = acceptedExtensions.ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileExtensionImportGroupFilter"/> class. 
    /// </summary>
    /// <param name="acceptedExtension">The accepted file extension.</param>
    public FileExtensionImportGroupFilter(string acceptedExtension)
    {
        _AcceptedExtensions = [acceptedExtension];
    }

    /// <inheritdoc />
    public ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context)
    {
        return importGroup.PrimaryFile.HasAnyExtension(_AcceptedExtensions)
            ? ValueTask.FromResult(FilterResult.Import)
            : ValueTask.FromResult(FilterResult.None);
    }
}