#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents an error in the context of creating import data.
/// </summary>
public class ImportDataException : PluginException
{
    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImportDataException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ImportDataException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImportDataException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception
    /// is specified.
    /// </param>
    public ImportDataException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    #endregion
}