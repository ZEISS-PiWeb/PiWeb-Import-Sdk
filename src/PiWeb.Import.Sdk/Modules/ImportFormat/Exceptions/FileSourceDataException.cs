namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat.Exceptions;

#region usings

using System;

#endregion

/// <summary>
/// Represents an error that occured during <see cref="IFileSource.GetDataStream"/> or <see cref="IFileSource.GetData"/>.
/// </summary>
public class FileSourceDataException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileSourceDataException"/> class.
    /// </summary>
    public FileSourceDataException(IFileSource fileSource, string? message = null, Exception? innerException = null)
        : base(message ?? "Could not read file source stream", innerException)
    {
        FileSource = fileSource ?? throw new ArgumentNullException(nameof(fileSource));
    }

    /// <summary>
    /// The <see cref="IFileSource"/> that caused the error.
    /// </summary>
    public IFileSource FileSource { get; }
}