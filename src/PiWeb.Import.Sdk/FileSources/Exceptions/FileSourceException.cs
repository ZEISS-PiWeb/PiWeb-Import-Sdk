#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.FileSources.Exceptions;

/// <summary>
/// Represents an error that occured during <see cref="IFileSource.GetDataStream"/>.
/// </summary>
public class FileSourceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileSourceException"/> class.
    /// </summary>
    public FileSourceException(IFileSource fileSource, string? message = null, Exception? innerException = null)
        : base(message ?? "Could not read file source stream", innerException)
    {
        FileSource = fileSource ?? throw new ArgumentNullException(nameof(fileSource));
    }

    /// <summary>
    /// The <see cref="IFileSource"/> that caused the error.
    /// </summary>
    public IFileSource FileSource { get; }
}