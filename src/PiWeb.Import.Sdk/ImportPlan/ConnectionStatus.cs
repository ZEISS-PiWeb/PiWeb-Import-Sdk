#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportPlan;

/// <summary>
/// Represents the state of the connection to the target server.
/// </summary>
public enum ConnectionStatus
{
	/// <summary>
	/// The status cannot be determined. E.g. cannot access authentication information.
	/// </summary>
	Undetermined,

	/// <summary>
	/// The server is available.
	/// </summary>
	Available,

	/// <summary>
	/// The server is not available.
	/// </summary>
	NotAvailable
}