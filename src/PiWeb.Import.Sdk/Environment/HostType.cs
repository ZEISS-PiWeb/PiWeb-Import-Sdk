#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Environment;

/// <summary>
/// Represents the different types of applications hosting a plugin.
/// </summary>
public enum HostType
{
	/// <summary>
	/// The plugin is hosted by an interactive application with user interface.
	/// </summary>
	UIApplication,

	/// <summary>
	/// The plugin is hosted by a non-interactive Windows service.
	/// </summary>
	Service
}