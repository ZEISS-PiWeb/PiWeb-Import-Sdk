﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.Diagnostics.CodeAnalysis;
using System.IO;

#endregion

/// <summary>
/// Represents a file.
/// </summary>
public interface IFileSource
{
    #region properties

    /// <summary>
    /// Gets the complete file name (including the extension) of this file.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the file name (without extension) of this file.
    /// </summary>
    public string BaseName { get; }

    /// <summary>
    /// Gets the file extension (including the period ".") of this file.
    /// </summary>
    /// <remarks>
    /// This method obtains the extension of path by searching path for a period ("."), starting from the last character in
    /// <see cref="Name"/> and continuing toward its first character. If a period is found before a directory separator, the returned
    /// string contains the period and the characters after it; otherwise, <c>string.Empty</c> is returned.
    /// </remarks>
    public string Extension { get; }

    /// <summary>
    /// Gets the relative path of this file. This path is relative to the corresponding import folder.
    /// </summary>
    public string RelativePath { get; }

    #endregion

    #region methods

    /// <summary>
    /// Indicates whether the file exists.
    /// </summary>
    public bool Exists();

    /// <summary>
    /// Tries to get the file data as <see cref="Stream"/>.
    /// </summary>
    /// <param name="dataStream">The file stream.</param>
    /// <returns><c>true</c> when the file could be read; otherwise <c>false</c>.</returns>
    bool TryGetDataStream([NotNullWhen(true)] out Stream? dataStream);

    /// <summary>
    /// Tries to get the file data as byte array.
    /// </summary>
    /// <param name="data">The file content as byte array.</param>
    /// <returns><c>true</c> when the file could be read; otherwise <c>false</c>.</returns>
    bool TryGetData([NotNullWhen(true)] out byte[]? data);

    #endregion
}