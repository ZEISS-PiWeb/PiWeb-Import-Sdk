#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Common.Logging;
using Zeiss.PiWeb.Sdk.Import.ImportHistory;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Represents the context of an import runner provided by the hosting application.
/// </summary>
public interface IParseContext
{
	#region properties

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