#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

using System.ComponentModel;

/// <summary>
/// Represents a configuration item for displaying a horizontal separator.
/// </summary>
public sealed class SeparatorItem : IConfigurationItem
{
	#region members

	private bool _IsVisible = true;

	#endregion

	#region interface IConfigurationItem

	/// <inheritdoc />
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <inheritdoc />
	public string Title => "";

	/// <inheritdoc />
	public required Section Section { get; init; }

	/// <inheritdoc />
	public string? Tooltip => null;

	/// <inheritdoc />
	public ConfigurationItemNotification Notification => ConfigurationItemNotification.None;

	/// <inheritdoc />
	public ConfigurationItemStatus Status => ConfigurationItemStatus.Valid;

	/// <inheritdoc />
	public bool? IsReadOnly => true;

	/// <inheritdoc />
	public bool IsVisible
	{
		get => _IsVisible;
		set
		{
			if( _IsVisible == value )
				return;

			_IsVisible = value;
			PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( nameof( IsVisible ) ) );
		}
	}

	/// <inheritdoc />
	public int Priority { get; init; }

	#endregion
}