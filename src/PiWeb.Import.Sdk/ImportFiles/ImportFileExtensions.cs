#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Zeiss.PiWeb.Import.Sdk.ImportFiles;

/// <summary>
/// Provides extension methods for the <see cref="IImportFile"/> interface.
/// </summary>
public static class ImportFileExtensions
{
    #region methods

    /// <summary>
    /// Checks whether the given import file has the given extension.
    /// </summary>
    /// <param name="importFile">The import file to check.</param>
    /// <param name="extension">The file extension. The extension may or may not include the initial period "."</param>
    /// <returns><c>true</c> if the file has the extension; otherwise <c>false</c>.</returns>
    public static bool HasExtension(this IImportFile importFile, string extension)
    {
        if (!extension.StartsWith('.'))
            extension = '.' + extension;

        return importFile.Name.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks whether the given import file has any of the given extensions.
    /// </summary>
    /// <param name="importFile">The import file to check.</param>
    /// <param name="extensions">The file extensions. The extension may or may not include the initial period "."</param>
    /// <returns><c>true</c> if the file has any of the extensions; otherwise <c>false</c>.</returns>
    public static bool HasAnyExtension(this IImportFile importFile, IEnumerable<string> extensions)
    {
        return extensions.Any(importFile.HasExtension);
    }

    #endregion
}