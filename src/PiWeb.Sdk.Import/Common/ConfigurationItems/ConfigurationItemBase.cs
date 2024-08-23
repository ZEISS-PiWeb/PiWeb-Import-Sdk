#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Represents the base implementation for <see cref="IConfigurationItem"/> which is recommended for any configuration item.
/// </summary>
public abstract class ConfigurationItemBase : IConfigurationItem
{
	#region members

	private string _Title = default!;
	private string? _Tooltip;
	private ConfigurationItemNotification _Notification = ConfigurationItemNotification.None;
	private ConfigurationItemStatus _Status = ConfigurationItemStatus.Valid;
	private bool? _IsReadOnly;
	private bool _IsVisible = true;
	private int _Priority;

	#endregion

	#region methods

	/// <summary>
	/// Raises the changed event for a given property.
	/// </summary>
	/// <param name="propertyName">The name of the property that changed.</param>
	protected virtual void RaisePropertyChanged( [CallerMemberName] string? propertyName = null )
	{
		PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
	}

	/// <summary>
	/// Updates the value of a property and raises a change event if the new value is different to the previous value.
	/// </summary>
	/// <param name="field">The backing field to update.</param>
	/// <param name="value">The new property value.</param>
	/// <param name="propertyName">The name of the property to change.</param>
	/// <typeparam name="T">The type of the property.</typeparam>
	/// <returns>True if the new value was different from the previous value; otherwise, false.</returns>
	protected bool Set<T>( ref T field, T value, [CallerMemberName] string? propertyName = null )
	{
		if( EqualityComparer<T>.Default.Equals( field, value ) )
			return false;
		field = value;
		RaisePropertyChanged( propertyName );
		return true;
	}

	#endregion

	#region interface IConfigurationItem

	/// <inheritdoc />
	public required string Title
	{
		get => _Title;
		set => Set( ref _Title, value ?? throw new ArgumentNullException( nameof( value ) ) );
	}

	/// <inheritdoc />
	public required Section Section { get; init; }

	/// <inheritdoc />
	public string? Tooltip
	{
		get => _Tooltip;
		set => Set( ref _Tooltip, value );
	}

	/// <inheritdoc />
	public ConfigurationItemNotification Notification
	{
		get => _Notification;
		set => Set( ref _Notification, value );
	}

	/// <inheritdoc />
	public ConfigurationItemStatus Status
	{
		get => _Status;
		set => Set( ref _Status, value );
	}

	/// <inheritdoc />
	public bool? IsReadOnly
	{
		get => _IsReadOnly;
		set => Set( ref _IsReadOnly, value );
	}

	/// <inheritdoc />
	public bool IsVisible
	{
		get => _IsVisible;
		set => Set( ref _IsVisible, value );
	}

	/// <inheritdoc />
	public int Priority
	{
		get => _Priority;
		set => Set( ref _Priority, value );
	}

	/// <inheritdoc />
	public event PropertyChangedEventHandler? PropertyChanged;

	#endregion
}