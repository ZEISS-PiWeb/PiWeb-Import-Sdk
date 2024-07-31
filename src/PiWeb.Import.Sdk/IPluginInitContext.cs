#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk;

using Zeiss.PiWeb.Import.Sdk.Environment;
using Zeiss.PiWeb.Import.Sdk.Logging;

/// <summary>
/// Represents the context of a plugin provided by the hosting application.
/// </summary>
public interface IPluginInitContext
{
	#region properties

	/// <summary>
	/// Contains information about the environment the plugin is hosted in.
	/// </summary>
	IEnvironmentInfo EnvironmentInfo { get; }
    
	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
	/// hosting application.
	/// </summary>
	ILogger Logger { get; }

	#endregion
}