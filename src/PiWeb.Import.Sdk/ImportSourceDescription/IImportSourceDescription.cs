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

/// <summary>
/// Represents a displayable description of an import source and its configurable properties.
/// </summary>
public interface IImportSourceDescription
{
	#region properties

	/// <summary>
	/// The display name of the import source.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// A list of configuration properties of the import source consisting of label and value pairs.
	/// </summary>
	ImmutableList<DescriptionProperty> DescriptionItems { get; }

	/// <summary>
	/// Raised after any property changed.
	/// </summary>
	event EventHandler? Changed;

	#endregion
}

/// <summary>
/// Represents a single property of displayable description of an import source.
/// </summary>
public struct DescriptionProperty
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="DescriptionProperty"/> class.
	/// </summary>
	public DescriptionProperty( string label, string text )
	{
		Label = label;
		Text = text;
	}

	#endregion

	#region properties

	/// <summary>
	/// The label to identify the item.
	/// </summary>
	public string Label { get; }

	/// <summary>
	/// The text displayed next to the label.
	/// </summary>
	public string Text { get; }

	#endregion
}