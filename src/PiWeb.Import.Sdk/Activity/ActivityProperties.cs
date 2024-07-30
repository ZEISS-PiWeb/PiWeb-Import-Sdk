#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Activity;

/// <summary>
/// Represents the basic properties of an import activity.
/// </summary>
public class ActivityProperties
{
	/// <summary>
	/// The type of the activity.
	/// </summary>
	public ActivityType ActivityType { get; init; } = ActivityType.Normal;

	/// <summary>
	/// The detailed text to display. A localization handler will be used to localize this text.
	/// Implement <see cref="IPlugin.CreateLocalizationHandler"/> to specify your own localization and formatting.
	/// </summary>
	public string DetailedDisplayText { get; init; } = string.Empty;

	/// <summary>
	/// The short text to display. A localization handler will be used to localize this text with the given
	/// arguments. Implement <see cref="IPlugin.CreateLocalizationHandler"/> to specify your own localization and
	/// formatting.
	/// </summary>
	public string ShortDisplayText { get; init; } = string.Empty;

	/// <summary>
	/// Indicates whether this activity resulted from an import source error such as a missing import source.
	/// This property is only respected when <see cref="ActivityType"/> is <see cref="Activity.ActivityType.Suspension"/>.
	/// </summary>
	public bool IsSourceProblem { get; init; } = false;

	/// <summary>
	/// Indicates whether this activity resulted from an import target error such as an unreachable target server.
	/// This property is only respected when <see cref="ActivityType"/> is <see cref="Activity.ActivityType.Suspension"/>.
	/// </summary>
	public bool IsTargetProblem { get; init; } = false;
}