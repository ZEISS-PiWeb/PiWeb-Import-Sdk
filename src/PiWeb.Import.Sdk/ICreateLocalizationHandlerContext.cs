#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Import.Sdk.Environment;
using Zeiss.PiWeb.Import.Sdk.LocalizationHandler;
using Zeiss.PiWeb.Import.Sdk.Logging;

namespace Zeiss.PiWeb.Import.Sdk;

/// <summary>
/// Represents the context for creating an <see cref="ILocalizationHandler"/> instance.
/// </summary>
public interface ICreateLocalizationHandlerContext
{
    /// <summary>
    /// Contains information about the environment the plugin is hosted in.
    /// </summary>
    IEnvironmentInfo EnvironmentInfo { get; }
    
    /// <summary>
    /// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
    /// hosting application.
    /// </summary>
    ILogger Logger { get; }
}