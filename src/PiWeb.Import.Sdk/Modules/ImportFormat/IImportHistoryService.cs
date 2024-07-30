#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Responsible for adding log messages to the import history entry of the current import.
/// The import history is maintained to track successful and faulty imports. Each import creates a new entry in the
/// import history.
/// Note: Adding messages with error severity leads to the import being canceled before any data is imported.
/// </summary>
public interface IImportHistoryService
{
    /// <summary>
    /// Adds a message to the import history entry of the current import.<br/>
    /// Note: Adding messages with error severity leads to the import being canceled before any data is imported.
    /// </summary>
    /// <param name="displayText">
    /// The display text of the message. A localization handler will be used to localize this text with the given
    /// arguments. Implement <see cref="IPlugin.CreateLocalizationHandler"/> to specify your own localization and
    /// formatting.
    /// </param>
    /// <param name="formatArgs">
    /// Format arguments used when formatting the display text of message.
    /// </param>
    /// <param name="severity">The severity of the message.</param>
    public void AddMessage(MessageSeverity severity, string displayText, params object[] formatArgs);
}