#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk;

using Environment;
using Logging;
using Modules.ImportAutomation;
using Modules.ImportFormat;

/// <summary>
/// Represents the context of a plugin provided by the hosting application.
/// </summary>
public interface IPluginContext
{
	#region properties

	/// <summary>
	/// Contains information about the environment a plugin is hosted in.
	/// </summary>
	IEnvironmentInfo EnvironmentInfo { get; }
    
	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the hosting application.
	/// </summary>
	ILogger Logger { get; }

	/// <summary>
	/// Registers an import automation.
	/// </summary>
	/// <param name="id">The id of the automation.</param>
	/// <param name="importAutomation">The import automation to register.</param>
	/// <exception cref="ModuleRegistrationException">Thrown when the import automation cannot be registered.</exception>
	void RegisterImportAutomation( string id, IImportAutomation importAutomation );

	/// <summary>
	/// Registers an import format.
	/// </summary>
	/// <param name="id">The id of the import format.</param>
	/// <param name="importFormat">The import format to register.</param>
	/// <exception cref="ModuleRegistrationException">Thrown when the import format cannot be registered.</exception>
	void RegisterImportFormat(string id, IImportFormat importFormat);

	#endregion
}