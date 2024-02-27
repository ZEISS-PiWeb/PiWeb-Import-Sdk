#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportPlan;

/// <summary>
/// A list of possible import target types.
/// </summary>
public enum ConnectionType
{
	/// <summary>
	/// The connection is to a file, e.g. a dfs file.
	/// </summary>
	File,

	/// <summary>
	/// The import target is a webservice.
	/// </summary>
	Webservice
}