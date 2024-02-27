#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using PropertyStorage;

/// <summary>
/// Represents a configuration item for selecting an entry from a given list of options.
/// </summary>
public class SelectConfigurationItem : ConfigurationItemBase
{
	#region members

	private string _Value;

	#endregion

	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="SelectConfigurationItem"/> class.
	/// </summary>
	/// <param name="defaultValue">The default value.</param>
	public SelectConfigurationItem( string defaultValue = "" )
	{
		Options.CollectionChanged += OptionsOnCollectionChanged;

		_Value = defaultValue;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SelectConfigurationItem"/> class.
	/// Associates this configuration item to a property of the given <paramref name="storage"/>.
	/// </summary>
	/// <param name="storage">The property storage to read from and write to.</param>
	/// <param name="key">The key of the property to associate the configuration item with.</param>
	/// <param name="defaultValue">The default value that is used if the property does not exist in the storage.</param>
	public SelectConfigurationItem( IPropertyStorage storage, string key, string defaultValue )
	{
		ArgumentNullException.ThrowIfNull( storage );
		ArgumentNullException.ThrowIfNull( key );

		Options.CollectionChanged += OptionsOnCollectionChanged;

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

	/// <summary>
	/// Gets a collection specifying the available options of this configuration item.
	/// </summary>
	public ObservableCollection<SelectableOption> Options { get; } = new ObservableCollection<SelectableOption>();

	#endregion

	#region methods

	private void OptionsOnCollectionChanged( object? sender, NotifyCollectionChangedEventArgs e )
	{
		RaisePropertyChanged( nameof( Options ) );
	}

	#endregion
}