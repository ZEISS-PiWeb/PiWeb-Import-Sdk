#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Possible types of notification of a configuration item.
/// </summary>
public enum NotificationType
{
	/// <summary>
	/// No special status.
	/// </summary>
	None,

	/// <summary>
	/// Warning status.
	/// </summary>
	Warning,

	/// <summary>
	/// Error status.
	/// </summary>
	Error
}