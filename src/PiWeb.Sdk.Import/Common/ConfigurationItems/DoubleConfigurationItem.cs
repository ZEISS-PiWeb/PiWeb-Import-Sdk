﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using Zeiss.PiWeb.Sdk.Common.PropertyStorage;

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Represents a configuration item for a double property.
/// </summary>
public class DoubleConfigurationItem : ConfigurationItemBase
{
	#region members

	private double _Value;

	#endregion

	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="DoubleConfigurationItem"/> class.
	/// </summary>
	/// <param name="defaultValue">The default value.</param>
	public DoubleConfigurationItem( double defaultValue = default )
	{
		_Value = defaultValue;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DoubleConfigurationItem"/> class.
	/// Associates this configuration item to a property of the given <paramref name="storage"/>.
	/// </summary>
	/// <param name="storage">The property storage to read from and write to.</param>
	/// <param name="key">The key of the property to associate the configuration item with.</param>
	/// <param name="defaultValue">The default value that is used if the property does not exist in the storage.</param>
	public DoubleConfigurationItem( IPropertyStorage storage, string key, double defaultValue )
	{
		ArgumentNullException.ThrowIfNull( storage );
		ArgumentNullException.ThrowIfNull( key );

		if( !storage.TryReadNumber<double>( key, out var existingValue ) )
		{
			existingValue = defaultValue;
			storage.WriteNumber( key, existingValue );
		}
		_Value = existingValue;

		storage.Changed += ( _, _ ) =>
		{
			Value = storage.ReadNumber( key, defaultValue );
		};

		PropertyChanged += ( _, e ) =>
		{
			if( e.PropertyName == nameof ( Value ) )
				storage.WriteNumber( key, _Value );
		};
	}

	#endregion

	#region properties

	/// <summary>
	/// Gets or sets the double value of this configuration item.
	/// </summary>
	public double Value
	{
		get => _Value;
		set => Set( ref _Value, value );
	}

	/// <summary>
	/// Gets the minimum value the user can enter.
	/// </summary>
	public double Minimum { get; init; } = double.MinValue;

	/// <summary>
	/// Gets the maximum value the user can enter.
	/// </summary>
	public double Maximum { get; init; } = double.MaxValue;

	/// <summary>
	/// Gets the step value for increasing or decreasing the current value.
	/// </summary>
	public double Step { get; init; } = 1;

	/// <summary>
	/// Gets the string format for formatting the number.
	/// </summary>
	/// <remarks>See also: https://learn.microsoft.com/en-us/dotnet/api/system.string.format</remarks>
	public string? NumberFormat { get; init; }

	#endregion
}