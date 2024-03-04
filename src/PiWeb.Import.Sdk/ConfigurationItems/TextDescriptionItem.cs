#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents a configuration item for displaying a readonly description text.
/// </summary>
public sealed class TextDescriptionItem : IConfigurationItem
{
	#region members

	private bool _IsVisible = true;
	private string _Text = default!;

	#endregion

	#region properties

	/// <summary>
	/// Gets or sets the text to display.
	/// </summary>
	public required string Text
	{
		get => _Text;
		set => Set( ref _Text, value );
	}

	#endregion

	#region methods

	private void Set<T>( ref T field, T value, [CallerMemberName] string? propertyName = null )
	{
		if( EqualityComparer<T>.Default.Equals( field, value ) )
			return;
		field = value;
		PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
	}

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
		set => Set( ref _IsVisible, value );
	}

	/// <inheritdoc />
	public int Priority { get; init; }

	#endregion
}