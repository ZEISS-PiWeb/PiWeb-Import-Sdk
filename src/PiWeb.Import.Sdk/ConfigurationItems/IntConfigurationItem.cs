#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

using System;
using PropertyStorage;

/// <summary>
/// Represents a configuration item for an integer property.
/// </summary>
public class IntConfigurationItem : ConfigurationItemBase
{
	#region members

	private int _Value;

	#endregion

	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="IntConfigurationItem"/> class.
	/// </summary>
	/// <param name="defaultValue">The default value.</param>
	public IntConfigurationItem( int defaultValue = default )
	{
		_Value = defaultValue;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="IntConfigurationItem"/> class.
	/// Associates this configuration item to a property of the given <paramref name="storage"/>.
	/// </summary>
	/// <param name="storage">The property storage to read from and write to.</param>
	/// <param name="key">The key of the property to associate the configuration item with.</param>
	/// <param name="defaultValue">The default value that is used if the property does not exist in the storage.</param>
	public IntConfigurationItem( IPropertyStorage storage, string key, int defaultValue )
	{
		ArgumentNullException.ThrowIfNull( storage );
		ArgumentNullException.ThrowIfNull( key );

		if( !storage.TryReadNumber<int>( key, out var existingValue ) )
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
	/// Gets or sets the integer value of this configuration item.
	/// </summary>
	public int Value
	{
		get => _Value;
		set => Set( ref _Value, value );
	}

	/// <summary>
	/// Gets the minimum value the user can enter.
	/// </summary>
	public int Minimum { get; init; } = int.MinValue;

	/// <summary>
	/// Gets the maximum value the user can enter.
	/// </summary>
	public int Maximum { get; init; } = int.MaxValue;

	/// <summary>
	/// Gets the step value for increasing or decreasing the current value.
	/// </summary>
	public int Step { get; init; } = 1;

	/// <summary>
	/// Gets the string format for formatting the number.
	/// </summary>
	/// <remarks>See also: https://learn.microsoft.com/en-us/dotnet/api/system.string.format</remarks>
	public string? NumberFormat { get; init; }

	#endregion
}