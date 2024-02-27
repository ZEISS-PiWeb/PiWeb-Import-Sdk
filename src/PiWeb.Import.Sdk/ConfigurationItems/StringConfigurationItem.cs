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
/// Represents a configuration item for a string property.
/// </summary>
public class StringConfigurationItem : ConfigurationItemBase
{
	#region members

	private string _Value;

	#endregion

	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="StringConfigurationItem"/> class.
	/// </summary>
	/// <param name="defaultValue">The default value.</param>
	public StringConfigurationItem( string defaultValue = "" )
	{
		_Value = defaultValue;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="StringConfigurationItem"/> class.
	/// Associates this configuration item to a property of the given <paramref name="storage"/>.
	/// </summary>
	/// <param name="storage">The property storage to read from and write to.</param>
	/// <param name="key">The key of the property to associate the configuration item with.</param>
	/// <param name="defaultValue">The default value that is used if the property does not exist in the storage.</param>
	public StringConfigurationItem( IPropertyStorage storage, string key, string defaultValue )
	{
		ArgumentNullException.ThrowIfNull( storage );
		ArgumentNullException.ThrowIfNull( key );

		if( !storage.TryReadString( key, out var existingValue ) )
		{
			existingValue = defaultValue;
			storage.WriteString( key, existingValue );
		}
		_Value = existingValue;

		storage.Changed += ( _, _ ) =>
		{
			Value = storage.ReadString( key, defaultValue );
		};

		PropertyChanged += ( _, e ) =>
		{
			if( e.PropertyName == nameof( Value ) )
				storage.WriteString( key, _Value );
		};
	}

	#endregion

	#region properties

	/// <summary>
	/// Gets or sets the value of this configuration item.
	/// </summary>
	public string Value
	{
		get => _Value;
		set => Set( ref _Value, value );
	}

	#endregion
}