#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

using System.ComponentModel;

/// <summary>
/// Represents the interface for specifying configuration UI for an import plan.
/// </summary>
public interface IConfigurationItem : INotifyPropertyChanged
{
	/// <summary>
	/// The display title of the configuration item.
	/// </summary>
	public string Title { get; }

	/// <summary>
	/// The section this configuration item belongs to.
	/// </summary>
	public Section Section { get; }

	/// <summary>
	/// The tooltip shown when a user hovers over the configuration item title.
	/// </summary>
	public string? Tooltip { get; }

	/// <summary>
	/// Defines a notification displayed for this configuration item. Notifications are used to display additional information about
	/// the current state of a configuration item.
	/// </summary>
	public ConfigurationItemNotification Notification { get; }

	/// <summary>
	/// The status of this configuration item. The status specifies whether the current configuration value is valid.
	/// An import plan can only be run when all its configuration items have a valid value. Setting this status does not automatically show
	/// a visual notification in the UI. Use <see cref="Notification"/> for visual feedback.
	/// </summary>
	public ConfigurationItemStatus Status { get; }

	/// <summary>
	/// Indicates whether this configuration item can be edited currently.
	/// When this value is set to <c>null</c>, the read only state is determined by whether the import plan is currently editable.
	/// </summary>
	public bool? IsReadOnly { get; }

	/// <summary>
	/// A value indicating whether this configuration item is currently visible to the user.
	/// </summary>
	public bool IsVisible { get; }

	/// <summary>
	/// The priority value used for ordering this configuration item relative to other configuration items in the same section.
	/// </summary>
	public int Priority { get; }
}