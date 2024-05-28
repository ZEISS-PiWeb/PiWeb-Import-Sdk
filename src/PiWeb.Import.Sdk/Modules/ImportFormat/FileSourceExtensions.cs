#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

/// <summary>
/// Provides methods to work with <see cref="IFileSource"/> more easily.
/// </summary>
public static class FileSourceExtensions
{
    #region methods

    /// <summary>
    /// Checks if <paramref name="fileSource"/> has the requested file extension <paramref name="extension"/>.
    /// </summary>
    /// <param name="fileSource">The <see cref="IFileSource"/> to check.</param>
    /// <param name="extension">The file extension (including the period ".").</param>
    /// <returns><c>true</c> if the file has the extension; otherwise <c>false</c>.</returns>
    public static bool HasExtension(this IFileSource fileSource, string extension)
    {
        if (!extension.StartsWith('.'))
            extension = '.' + extension;

        return fileSource.Name.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if <paramref name="fileSource"/> has any of the requested file extension <paramref name="extensions"/>.
    /// </summary>
    /// <param name="fileSource">The <see cref="IFileSource"/> to check.</param>
    /// <param name="extensions">The file extensions (including the period ".").</param>
    /// <returns><c>true</c> if the file has any of  the extensions; otherwise <c>false</c>.</returns>
    public static bool HasAnyExtension(this IFileSource fileSource, IEnumerable<string> extensions)
    {
        return extensions.Any(extension => fileSource.HasExtension(extension));
    }

    /// <summary>
    /// Gets the first <see cref="IFileSource"/> within <paramref name="fileSources"/> that has any of the requested file extension in
    /// <paramref name="extensions"/>.
    /// </summary>
    /// <param name="fileSources">The <see cref="IFileSource"/>s to check.</param>
    /// <param name="extensions">The file extensions (including the period ".").</param>
    /// <returns>The first matching <see cref="IFileSource"/>; otherwise <c>null</c>.</returns>
    public static IFileSource? GetFirstWithExtension(this IEnumerable<IFileSource> fileSources, ISet<string> extensions)
    {
        return fileSources.FirstOrDefault(fileSource => fileSource.HasAnyExtension(extensions));
    }

    /// <summary>
    /// Gets the first <see cref="IFileSource"/> within <paramref name="fileSources"/> that has the requested <paramref name="extensions"/>.
    /// </summary>
    /// <param name="fileSources">The <see cref="IFileSource"/>s to check.</param>
    /// <param name="extensions">The file extension (including the period ".").</param>
    /// <returns>The first matching <see cref="IFileSource"/>; otherwise <c>null</c>.</returns>
    public static IFileSource? GetFirstWithExtension(this IEnumerable<IFileSource> fileSources, string extensions)
    {
        return fileSources.FirstOrDefault(fileSource => fileSource.HasExtension(extensions));
    }

    #endregion
}