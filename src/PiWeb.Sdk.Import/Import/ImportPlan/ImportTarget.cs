#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportPlan;

/// <summary>
/// Represents an import target configuration.
/// </summary>
public class ImportTarget
{
	#region properties

	/// <summary>
	/// The type of the connection.
	/// </summary>
	public required ConnectionType Type { get; init; }

	/// <summary>
	/// The address of the webservice. Only valid when <see cref="ConnectionType"/> is <see cref="ConnectionType.Webservice"/>.
	/// </summary>
	public required string ServiceAddress { get; init; }

	/// <summary>
	/// The root part of the connection in RoundtripString format.
	/// </summary>
	public required string RootPart { get; init; }

	/// <summary>
	/// The authentication information required to connect.
	/// </summary>
	public required IAuthData AuthData { get; init; }

	#endregion
}