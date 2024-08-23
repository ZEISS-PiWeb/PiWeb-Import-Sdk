#region copyright

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
/// Represents a configuration item for true/false property.
/// </summary>
public class BoolConfigurationItem : ConfigurationItemBase
{
	#region members

	private bool _Value;

	#endregion

	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="BoolConfigurationItem"/> class.
	/// </summary>
	/// <param name="defaultValue">The default value.</param>
	public BoolConfigurationItem( bool defaultValue = false )
	{
		_Value = defaultValue;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="BoolConfigurationItem"/> class.
	/// Associates this configuration item to a property of the given <paramref name="storage"/>.
	/// </summary>
	/// <param name="storage">The property storage to read from and write to.</param>
	/// <param name="key">The key of the property to associate the configuration item with.</param>
	/// <param name="defaultValue">The default value that is used if the property does not exist in the storage.</param>
	public BoolConfigurationItem( IPropertyStorage storage, string key, bool defaultValue )
	{
		ArgumentNullException.ThrowIfNull( storage );
		ArgumentNullException.ThrowIfNull( key );

		if( !storage.TryReadBool( key, out var existingValue ) )
		{
			existingValue = defaultValue;
			storage.WriteBool( key, existingValue );
		}
		_Value = existingValue;

		storage.Changed += ( _, _ ) =>
		{
			Value = storage.ReadBool( key, defaultValue );
		};

		PropertyChanged += ( _, e ) =>
		{
			if( e.PropertyName == nameof ( Value ) )
				storage.WriteBool( key, _Value );
		};
	}

	#endregion

	#region properties

	/// <summary>
	/// Gets or sets the true/false value of this configuration item.
	/// </summary>
	public bool Value
	{
		get => _Value;
		set => Set( ref _Value, value );
	}

	#endregion
}