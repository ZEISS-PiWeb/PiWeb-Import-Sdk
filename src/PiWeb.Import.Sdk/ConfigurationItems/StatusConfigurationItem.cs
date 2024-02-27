#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

/// <summary>
/// Represents a configuration item that displays a status (e.g. error, warning) with a text.
/// </summary>
public class StatusConfigurationItem : ConfigurationItemBase
{
	#region members

	private string? _Text;
	private StatusDisplayType _StatusDisplay;

	#endregion

	#region properties

	/// <summary>
	/// The display text or <c>null</c> when a default text should be used.
	/// </summary>
	public string? Text
	{
		get => _Text;
		set => Set( ref _Text, value );
	}

	/// <summary>
	/// The status display, e.g. error or ok.
	/// </summary>
	public StatusDisplayType StatusDisplay
	{
		get => _StatusDisplay;
		set => Set( ref _StatusDisplay, value );
	}

	#endregion
}