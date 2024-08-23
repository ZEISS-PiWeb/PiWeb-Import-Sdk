#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Represents a grouping for configuration items.
/// </summary>
public sealed class Section
{
	#region properties

	/// <summary>
	/// The display title of this section.
	/// </summary>
	public required string Title { get; init; }

	/// <summary>
	/// The priority value of this section. Sections with lower priority values are placed above sections with higher priority values
	/// in the UI.
	/// </summary>
	public required int Priority { get; init; }

	#endregion
}