#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Import.Sdk.ImportHistory;
using Zeiss.PiWeb.Import.Sdk.Logging;

namespace Zeiss.PiWeb.Import.Sdk.ImportFiles;

/// <summary>
/// Holds contextual information that might be required when filtering and completing groups of import files.
/// </summary>
public interface IFilterContext
{
    /// <summary>
    /// The import folder of the current import group. Use this folder as source for additional import files
    /// if the format requires it.
    /// </summary>
    IImportFolder CurrentImportFolder { get; }

    /// <summary>
    /// A service to edit the import history entry of the current import group. 
    /// </summary>
    IImportHistoryService ImportHistoryService { get; }
    
    /// <summary>
    /// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
    /// hosting application.
    /// </summary>
    ILogger Logger { get; }
}