#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using Zeiss.PiWeb.Import.Sdk.ImportFiles;
using Zeiss.PiWeb.Import.Sdk.Logging;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents the context for the creation of <see cref="IImportGroupFilter"/> instances. This context will be
/// provided by the hosting application. 
/// </summary>
public interface ICreateImportGroupFilterContext
{
    #region properties

    /// <summary>
    /// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
    /// hosting application.
    /// </summary>
    ILogger Logger { get; }

    #endregion
}