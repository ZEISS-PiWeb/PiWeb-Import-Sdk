#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Immutable;

namespace Zeiss.PiWeb.Sdk.Import.ImportSourceDescription;

/// <summary>
/// Represents a displayable description of an import source and its configurable properties.
/// </summary>
public interface IImportSourceDescription
{
	#region properties

	/// <summary>
	/// The display name of the import source.
	/// </summary>
	string Name { get; set; }

	/// <summary>
	/// A list of configuration properties of the import source consisting of label and value pairs.
	/// </summary>
	ImmutableList<DescriptionProperty> DescriptionItems { get; }

	#endregion

	#region methods

	/// <summary>
	/// Adds the given label and text as description property at the end of the property list. If a description property with the given
	/// label already exists (case-sensitive ordinal comparison), its text is replaced with the given text instead.
	/// </summary>
	/// <param name="label">The label of the description property.</param>
	/// <param name="text">The text of the description property.</param>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	bool AddOrReplaceProperty(string label, string text);

	/// <summary>
	/// Updates the list of description properties via a given transformer function.
	/// </summary>
	/// <param name="transformer">The transformer function to apply.</param>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	bool UpdateProperties(Func<ImmutableList<DescriptionProperty>, ImmutableList<DescriptionProperty>> transformer);

	/// <summary>
	/// Removes the existing property (case-sensitive ordinal comparison) with the given label from the property list.
	/// If no such property exists, the operation has no effect.
	/// </summary>
	/// <param name="label">The label of the property to remove.</param>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	bool RemoveProperty(string label);

	/// <summary>
	///	Removes all properties from the property list. If the property list is already empty, this operation has no effect.
	/// </summary>
	/// <returns>True if the operation caused changes; otherwise, false.</returns>
	bool ClearProperties();

	#endregion
}