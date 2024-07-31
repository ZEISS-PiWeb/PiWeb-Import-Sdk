#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Import.Sdk.Logging;

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents the context for the creation of <see cref="IImportParser"/> instances. This context will be
/// provided by the hosting application. 
/// </summary>
public interface ICreateImportParserContext
{
    #region properties

    /// <summary>
    /// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
    /// hosting application.
    /// </summary>
    ILogger Logger { get; }

    #endregion
}