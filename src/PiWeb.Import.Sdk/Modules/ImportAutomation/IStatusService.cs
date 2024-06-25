using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.Activity;

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

/// <summary>
/// Responsible for managing and displaying the current status (activity and recent events) of an
/// <see cref="IImportRunner"/>. A status service is usually connected to the user interface of the hosting application.   
/// </summary>
public interface IStatusService
{
    /// <summary>
    /// Posts a new entry to the event list. A list of the latest events of an import plan is shown on the status page
    /// of the Auto Importer UI.
    /// </summary>
    /// <param name="displayText">
    /// The display text of the event. This can be a format string.
    /// Localization and formatting of this text can optionally be customized by implementing <see cref="IImportAutomation.LocalizeText"/>
    /// and/or <see cref="IImportAutomation.FormatText"/> on the import automation. When these methods are not implemented, built in
    /// mechanisms are used.
    /// </param>
    /// <param name="formatArgs">
    /// Format arguments used to format the display text of the activity when this is a format string.
    /// </param>
    /// <param name="severity">The severity of the event.</param>
    public Task PostImportEventAsync( EventSeverity severity, string displayText, params object[] formatArgs );

    /// <summary>
    /// Sets the current activity. The current activity of an import plan is shown at various places of the Auto Importer UI either as
    /// detailed text or short text. Other properties of the current activity may also effect the display.
    /// If this new activity has a different detailed text than the previous one, an import event with the same text is posted to document
    /// the activity change. If the new activity is of suspension type, the event is posted as an error event.
    /// </summary>
    /// <param name="activityProperties">The properties of the activity to show.</param>
    /// <param name="formatArgs">
    /// Format arguments used to format detailed and short display text of the activity when these are format strings.
    /// </param>
    public Task SetActivityAsync( ActivityProperties activityProperties, params object[] formatArgs );

    /// <summary>
    /// Sets the current activity to no activity.
    /// </summary>
    public Task ClearActivityAsync();
}