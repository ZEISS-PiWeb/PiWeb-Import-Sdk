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
using System.Linq;

#endregion

/// <summary>
/// Provides methods to work with <see cref="IImportGroup"/> more easily.
/// </summary>
public static class ImportGroupExtensions
{
    #region methods

    /// <summary>
    /// Checks whether <paramref name="importGroup"/>'s <see cref="IImportGroup.PrimaryFile"/> has the requested <paramref name="extension"/>.
    /// </summary>
    /// <param name="importGroup">The group containing the primary file to check.</param>
    /// <param name="extension">The file extension (including the period ".").</param>
    /// <returns><c>true</c> if the <see cref="IImportGroup.PrimaryFile"/> has the extension; otherwise <c>false</c>.</returns>
    public static bool HasExtension(this IImportGroup importGroup, string extension)
    {
        return importGroup.PrimaryFile.HasExtension(extension);
    }

    /// <summary>
    /// Checks whether <paramref name="importGroup"/>'s <see cref="IImportGroup.PrimaryFile"/> has  any of the requested
    /// <paramref name="extensions"/>.
    /// </summary>
    /// <param name="importGroup">The group containing the primary file to check.</param>
    /// <param name="extensions">The file extensions (including the period ".").</param>
    /// <returns><c>true</c> if the <see cref="IImportGroup.PrimaryFile"/> has any of the extensions; otherwise <c>false</c>.</returns>
    public static bool HasAnyExtension(this IImportGroup importGroup, IEnumerable<string> extensions)
    {
        return extensions.Any(importGroup.PrimaryFile.HasExtension);
    }

    #endregion
}