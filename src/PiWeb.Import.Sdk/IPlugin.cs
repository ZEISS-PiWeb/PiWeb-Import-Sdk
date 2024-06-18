#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk;

using System.Threading.Tasks;

/// <summary>
/// Represents a PiWeb Auto Importer plugin.
/// </summary>
public interface IPlugin
{
	#region methods

	/// <summary>
	/// Initializes the plugin. Usually called during startup of the hosting application while showing a splash screen.
	/// Startup finishes when the returned task is completed.
	/// </summary>
	/// <param name="context">Contains information about the hosting application.</param>
	/// <exception cref="ModuleRegistrationException">Thrown when module registration fails.</exception>
	Task Init( IPluginContext context );

	#endregion
}