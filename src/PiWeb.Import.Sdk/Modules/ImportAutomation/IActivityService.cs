#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

/// <summary>
/// Responsible for managing and displaying the current status (activity and recent events) of an
/// <see cref="IImportRunner"/>. A status service is usually connected to the user interface of the hosting application.   
/// </summary>
public interface IActivityService
{
    /// <summary>
    /// Sets the current activity. The current activity of an import plan is shown at various places of the
    /// Auto Importer UI either in the form of the detailed text or the short text. Other properties of the current
    /// activity may also effect the display.
    /// If this new activity has a different detailed text than the previous one, an import event with the same text
    /// is posted to document the activity change. If the new activity is of suspension type, the event is posted as
    /// an error event.
    /// A localization handler will be used to localize the detailed text and the short text with the given format
    /// arguments. Implement <see cref="IPlugin.CreateLocalizationHandler"/> to specify your own localization and
    /// formatting.
    /// </summary>
    /// <param name="activityProperties">The properties of the activity to show.</param>
    /// <param name="formatArgs">
    /// Format arguments used when formatting the detailed text and the short text of the activity.
    /// </param>
    public void SetActivity( ActivityProperties activityProperties, params object[] formatArgs );

    /// <summary>
    /// Resets the current activity to no activity.
    /// </summary>
    public void ClearActivity();
    
    /// <summary>
    /// Posts a new entry to the list of recent events of an import plan. The event list of an import plan is shown
    /// on the status page of the Auto Importer UI.
    /// </summary>
    /// <param name="displayText">
    /// The display text of the event. A localization handler will be used to localize this text with the given
    /// arguments. Implement <see cref="IPlugin.CreateLocalizationHandler"/> to specify your own localization and
    /// formatting.
    /// </param>
    /// <param name="formatArgs">
    /// Format arguments used when formatting the display text of the activity.
    /// </param>
    /// <param name="severity">The severity of the event.</param>
    public void PostActivityEvent( EventSeverity severity, string displayText, params object[] formatArgs );
}