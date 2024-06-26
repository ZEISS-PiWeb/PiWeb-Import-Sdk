#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Import.Sdk.Environment;
using Zeiss.PiWeb.Import.Sdk.Logging;

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents the context of an import runner provided by the hosting application.
/// </summary>
public interface IParseContext
{
	#region properties

	/// <summary>
	/// Contains information about the environment a plugin is hosted in.
	/// </summary>
	IEnvironmentInfo EnvironmentInfo { get; }

	/// <summary>
	/// A service to edit the import history entry of the current import. 
	/// </summary>
	IImportHistoryService ImportHistoryService { get; }

	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file
	/// of the hosting application.
	/// </summary>
	ILogger Logger { get; }

	#endregion
}