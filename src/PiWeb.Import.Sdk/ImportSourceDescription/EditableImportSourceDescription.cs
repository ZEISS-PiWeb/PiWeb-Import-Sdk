#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportSourceDescription;

using System;
using System.Collections.Immutable;
using ImportSourceDescription;

/// <summary>
/// <inheritdoc />
/// This standard implementation provides thread safe operations for modification and implements change notifications.
/// </summary>
public class EditableImportSourceDescription : IImportSourceDescription
{
	#region members

	private ImmutableList<DescriptionProperty> _DescriptionProperties = ImmutableList<DescriptionProperty>.Empty;
	private string _Name = string.Empty;

	#endregion

	#region methods

	/// <summary>
	/// Adds the given label and text as description property at the end of the property list. If a description property with the given
	/// label already exists (case sensitive ordinal comparison), its text is replaced with the given text instead.
	/// If this operation causes any changes, the <see cref="Changed"/> event is raised.
	/// </summary>
	/// <param name="label">The label of the description property.</param>
	/// <param name="text">The text of the description property.</param>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	public virtual bool AddOrReplaceProperty( string label, string text )
	{
		var hasUpdate = ImmutableInterlocked.Update( ref _DescriptionProperties, properties =>
		{
			var existingIndex =
				properties.FindIndex( property => string.Equals( property.Label, label, StringComparison.Ordinal ) );

			if( existingIndex < 0 )
				return properties.Add( new DescriptionProperty( label, text ) );

			if( string.Equals( properties[ existingIndex ].Text, text ) )
				return properties;

			return properties.SetItem( existingIndex, new DescriptionProperty( label, text ) );
		} );

		if( hasUpdate )
			RaiseChanged();

		return hasUpdate;
	}

	/// <summary>
	/// Removes the existing property (case sensitive ordinal comparison) with the given label from the property list.
	/// If no such property exists, the operation has no effect.
	/// If this operation causes any changes, the <see cref="Changed"/> event is raised.
	/// </summary>
	/// <param name="label">The label of the property to remove.</param>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	public virtual bool RemoveProperty( string label )
	{
		var hasUpdate = ImmutableInterlocked.Update( ref _DescriptionProperties, properties =>
		{
			var existingIndex =
				properties.FindIndex( property => string.Equals( property.Label, label, StringComparison.Ordinal ) );

			if( existingIndex < 0 )
				return properties;

			return properties.RemoveAt( existingIndex );
		} );

		if( hasUpdate )
			RaiseChanged();

		return hasUpdate;
	}

	/// <summary>
	///	Removes all properties from the property list. If the property list is already empty, this operation has no effect.
	/// If this operation causes any changes, the <see cref="Changed"/> event is raised.
	/// </summary>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	public virtual bool ClearProperties()
	{
		var hasUpdate = ImmutableInterlocked.Update( ref _DescriptionProperties, properties => properties.Clear() );

		if( hasUpdate )
			RaiseChanged();

		return hasUpdate;
	}

	/// <summary>
	/// Updates the list of description properties via a given transformer function.
	/// If this operation causes any changes, the <see cref="Changed"/> event is raised.
	/// </summary>
	/// <param name="transformer">The transformer function to apply.</param>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	public virtual bool UpdateProperties( Func<ImmutableList<DescriptionProperty>, ImmutableList<DescriptionProperty>> transformer )
	{
		var hasUpdate = ImmutableInterlocked.Update( ref _DescriptionProperties, transformer );

		if( hasUpdate )
			RaiseChanged();

		return hasUpdate;
	}

	/// <summary>
	/// Raises the <see cref="Changed"/> event.
	/// </summary>
	protected void RaiseChanged()
	{
		Changed?.Invoke( this, EventArgs.Empty );
	}

	#endregion

	#region interface IImportSourceDescription

	/// <inheritdoc />
	public virtual string Name
	{
		get => _Name;
		set
		{
			_Name = value;
			RaiseChanged();
		}
	}

	/// <inheritdoc />
	public virtual ImmutableList<DescriptionProperty> DescriptionItems => _DescriptionProperties;

	/// <inheritdoc />
	public event EventHandler? Changed;

	#endregion
}