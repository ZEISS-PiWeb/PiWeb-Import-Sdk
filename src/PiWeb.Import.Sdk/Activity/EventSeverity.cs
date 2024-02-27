#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Activity;

/// <summary>
/// Represents the severity of an import event.
/// </summary>
public enum EventSeverity
{
	/// <summary>
	/// The event is of informational kind.
	/// </summary>
	Info = 0,

	/// <summary>
	/// The event presents an error.
	/// </summary>
	Error = 100
}