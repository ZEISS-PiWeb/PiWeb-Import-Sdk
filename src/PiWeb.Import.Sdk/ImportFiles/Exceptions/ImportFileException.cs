#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Import.Sdk.ImportFiles.Exceptions;

/// <summary>
/// Represents an error that occured during <see cref="IImportFile.GetDataStream"/>.
/// </summary>
public class ImportFileException : PluginException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImportFileException"/> class.
    /// </summary>
    public ImportFileException(IImportFile importFile, string? message = null, Exception? innerException = null)
        : base(message ?? "Could not read file source stream", innerException)
    {
        ImportFile = importFile ?? throw new ArgumentNullException(nameof(importFile));
    }

    /// <summary>
    /// The <see cref="IImportFile"/> that caused the error.
    /// </summary>
    public IImportFile ImportFile { get; }
}