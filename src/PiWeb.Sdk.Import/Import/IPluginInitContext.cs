#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Common.Environment;
using Zeiss.PiWeb.Sdk.Common.Logging;

namespace Zeiss.PiWeb.Sdk.Import;

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