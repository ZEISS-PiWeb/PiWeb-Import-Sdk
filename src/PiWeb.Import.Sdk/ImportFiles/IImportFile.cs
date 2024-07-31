#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System;
using System.IO;
using Zeiss.PiWeb.Import.Sdk.ImportFiles.Exceptions;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportFiles;

/// <summary>
/// Represents a file.
/// </summary>
public interface IImportFile
{
    #region properties

    /// <summary>
    /// The name of this file including the extension.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The name of this file without extension.
    /// </summary>
    public string BaseName { get; }

    /// <summary>
    /// The extension of the name of this file including the period (".").
    /// </summary>
    /// <remarks>
    /// The extension is obtained by searching the file name for a period ("."), starting from the last character
    /// and continuing toward its first character. If a period is found, the returned string contains the period
    /// and the characters after it; otherwise, <c>string.Empty</c> is returned.
    /// </remarks>
    public string Extension { get; }

    /// <summary>
    /// The time this file was last written to.
    /// </summary>
    DateTimeOffset LastWriteTime { get; }

    /// <summary>
    /// The creation time of the file.
    /// </summary>
    DateTimeOffset CreationTime { get; }
    
    #endregion

    #region methods

    /// <summary>
    /// Gets the file data as <see cref="Stream"/>.
    /// </summary>
    /// <returns>The file data stream.</returns>
    /// <exception cref="ImportFileException">Thrown when opening the file failed.</exception>
    Stream GetDataStream();

    #endregion
}