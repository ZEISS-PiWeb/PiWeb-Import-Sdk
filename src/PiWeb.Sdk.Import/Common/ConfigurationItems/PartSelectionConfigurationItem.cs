#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using Zeiss.PiWeb.Sdk.Common.PropertyStorage;

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Represents a configuration item for a property pointing to a specific part.
/// </summary>
public class PartSelectionConfigurationItem : ConfigurationItemBase
{
	#region members

	private string _Value;

	#endregion

	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="PartSelectionConfigurationItem"/> class.
	/// </summary>
	/// <param name="defaultValue">The default value.</param>
	public PartSelectionConfigurationItem( string defaultValue = "/" )
	{
		_Value = defaultValue;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="PartSelectionConfigurationItem"/> class.
	/// Associates this configuration item to a property of the given <paramref name="storage"/>.
	/// </summary>
	/// <param name="storage">The property storage to read from and write to.</param>
	/// <param name="key">The key of the property to associate the configuration item with.</param>
	/// <param name="defaultValue">The default value that is used if the property does not exist in the storage.</param>
	public PartSelectionConfigurationItem( IPropertyStorage storage, string key, string defaultValue = "/" )
	{
		ArgumentNullException.ThrowIfNull( storage );
		ArgumentNullException.ThrowIfNull( key );
		ArgumentNullException.ThrowIfNull( defaultValue );

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
			if( e.PropertyName == nameof ( Value ) )
				storage.WriteString( key, _Value );
		};
	}

	#endregion

	#region properties

	/// <summary>
	/// Gets or sets the path of the currently selected part of this configuration item.
	/// </summary>
	public string Value
	{
		get => _Value;
		set => Set( ref _Value, value );
	}

	#endregion
}