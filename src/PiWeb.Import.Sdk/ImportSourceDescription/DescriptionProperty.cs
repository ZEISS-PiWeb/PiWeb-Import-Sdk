#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportSourceDescription;

/// <summary>
/// Represents a single property of displayable description of an import source.
/// </summary>
public struct DescriptionProperty
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="DescriptionProperty"/> class.
	/// </summary>
	public DescriptionProperty(string label, string text)
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