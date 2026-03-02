#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Common.Logging;
using Zeiss.PiWeb.Sdk.Import.ImportController;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.ImportHistory;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Represents the context of an import runner provided by the hosting application.
/// </summary>
public interface IUploadContext
{
	#region properties

	/// <summary>
	/// A service to edit the import history entry of the current import. 
	/// </summary>
	IImportHistoryService ImportHistoryService { get; }

	/// <summary>
	/// The import controller. Can be used to modify the behavior of the current import.
	/// </summary>
	IImportController ImportController { get; }

	/// <summary>
	/// The import group of the current import.
	/// </summary>
	IImportGroup ImportGroup { get; }

	/// <summary>
	/// The path of the part this upload was originally targeted at in RoundtripString format.
	/// This path is directly determined by the import source.
	/// </summary>
	string OriginalImportTargetPartPath { get; }
	
	/// <summary>
	/// The path of the part this upload is finally targeted at in RoundtripString format.
	/// This path is created by applying the configured path rules to the original import target part path or
	/// the root path. 
	/// </summary>
	string ImportTargetPartPath { get; }

	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file
	/// of the hosting application.
	/// </summary>
	ILogger Logger { get; }

	#endregion
}