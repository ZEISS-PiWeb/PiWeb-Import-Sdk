#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportPlan;

/// <summary>
/// Represents the different execution modes of an import plan.
/// </summary>
public enum ExecutionMode
{
	/// <summary>
	/// The import plan is executed in process of the auto importer ui.
	/// </summary>
	InProcess,

	/// <summary>
	/// The import plan is executed as a service on this machine.
	/// </summary>
	Service
}