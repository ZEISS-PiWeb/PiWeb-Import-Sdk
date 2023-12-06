#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace ImportPluginBase.Exceptions;

/// <summary>
/// Represents an error in the context of Auto Importer plugins.
/// </summary>
public class PluginException : Exception
{
    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginException"/> class.
    /// </summary>
    public PluginException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PluginException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference if no inner exception
    /// is specified.
    /// </param>
    public PluginException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    #endregion
}