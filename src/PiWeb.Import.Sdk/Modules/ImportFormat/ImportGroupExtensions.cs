#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.Collections.Generic;

#endregion

/// <summary>
/// Provides extension methods for <see cref="ImportGroup"/>.
/// </summary>
public static class ImportGroupExtensions
{
    #region methods

    /// <summary>
    /// Creates an <see cref="ImportGroup"/> containing <paramref name="file"/> that has the given <see cref="State"/>.
    /// </summary>
    /// <param name="file">The file associated with this group. See <see cref="State"/> for more information.</param>
    /// <param name="state">The state of the resulting <see cref="ImportGroup"/>.</param>
    public static ImportGroup CreateImportGroup(this IFileSource file, State state)
    {
        var importGroup = new ImportGroup();
        importGroup.Files = importGroup.Files.Add(file);
        importGroup.State = state;

        return importGroup;
    }

    /// <summary>
    /// Creates an <see cref="ImportGroup"/> containing <paramref name="files"/> that has the given <see cref="State"/>.
    /// </summary>
    /// <param name="files">The files associated with this group. See <see cref="State"/> for more information.</param>
    /// <param name="state">The state of the resulting <see cref="ImportGroup"/>.</param>
    public static ImportGroup CreateImportGroup(this IEnumerable<IFileSource> files, State state)
    {
        var importGroup = new ImportGroup();
        importGroup.Files = importGroup.Files.AddRange(files);
        importGroup.State = state;

        return importGroup;
    }

    #endregion
}