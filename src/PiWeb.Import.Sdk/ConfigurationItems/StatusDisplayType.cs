#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

/// <summary>
/// Represents the different states a <see cref="StatusConfigurationItem"/> can have.
/// </summary>
public enum StatusDisplayType
{
	/// <summary>
	/// The status is unknown or irrelevant.
	/// </summary>
	Unknown,

	/// <summary>
	/// The status is ok.
	/// </summary>
	Ok,

	/// <summary>
	/// The Warning status.
	/// </summary>
	Warning,

	/// <summary>
	/// The Error status.
	/// </summary>
	Error
}