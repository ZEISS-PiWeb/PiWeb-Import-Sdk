#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

using System;
using ImportPlan;

/// <summary>
/// Represents the setup of the associated import plan.
/// </summary>
public interface IImportPlanSetup
{
	#region events

	/// <summary>
	/// Raised when any of the properties changed.
	/// </summary>
	event EventHandler? Changed;

	#endregion

	#region properties

	/// <summary>
	/// The id of the associated import plan.
	/// </summary>
	Guid ImportPlanId { get; }

	/// <summary>
	/// The import target or <c>null</c> when no import target is configured.
	/// </summary>
	ImportTarget? ImportTarget { get; }

	/// <summary>
	/// The import plan execution specific properties such as the execution mode.
	/// </summary>
	Execution Execution { get; }

	#endregion
}