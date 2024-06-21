#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.IO;
using Exceptions;

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
    /// Gets the file data as <see cref="Stream"/>.
    /// </summary>
    /// <returns>The file data stream.</returns>
    /// <exception cref="FileSourceDataException">When retrieving the data failed.</exception>
    Stream GetDataStream();

    /// <summary>
    /// Gets the file data as byte array.
    /// </summary>
    /// <returns>The file data as byte array.</returns>
    /// <exception cref="FileSourceDataException">When retrieving the data failed.</exception>
    byte[] GetData();

    #endregion
}