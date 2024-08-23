#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Represents a selectable option for a <see cref="SelectConfigurationItem"/>.
/// </summary>
public record SelectableOption
{
	/// <summary>
	/// Gets the key of this option.
	/// </summary>
	public required string Key { get; init; }

	/// <summary>
	/// Gets the display title of this option.
	/// </summary>
	public required string Title { get; init; }

	/// <summary>
	/// Gets a tooltip for this option.
	/// </summary>
	public string? Tooltip { get; init; }
}