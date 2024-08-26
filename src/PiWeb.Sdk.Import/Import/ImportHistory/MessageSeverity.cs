#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportHistory;

/// <summary>
/// Represents the severity of an import history message.
/// </summary>
public enum MessageSeverity
{
    /// <summary>
    /// The message is informational.
    /// </summary>
    Info = 0,

    /// <summary>
    /// The message represents a warning.
    /// </summary>
    Warning = 50,
    
    /// <summary>
    /// The message represents an error.
    /// </summary>
    Error = 100
}