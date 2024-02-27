#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

using System;
using System.Threading.Tasks;
using Activity;
using Environment;
using ImportPlan;
using Logging;
using PropertyStorage;

/// <summary>
/// Represents the context of an import runner provided by the hosting application.
/// </summary>
public interface IImportRunnerContext
{
	#region properties

	/// <summary>
	/// The id of the associated import plan.
	/// </summary>
	Guid ImportPlanId { get; }

	/// <summary>
	/// Contains information about the environment a plugin is hosted in.
	/// </summary>
	IEnvironmentInfo EnvironmentInfo { get; }

	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the hosting application.
	/// </summary>
	ILogger Logger { get; }

	/// <summary>
	/// The configured import target.
	/// </summary>
	ImportTarget ImportTarget { get; }

	/// <summary>
	/// The configuration property storage.
	/// </summary>
	public IPropertyReader PropertyStorage { get; }

	#endregion

	#region methods

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

	#endregion
}