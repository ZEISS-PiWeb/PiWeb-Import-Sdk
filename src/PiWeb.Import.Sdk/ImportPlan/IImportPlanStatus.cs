#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Import.Sdk.ImportPlan;

/// <summary>
/// Represents the status of the associated import plan.
/// </summary>
public interface IImportPlanStatus
{
	#region events

	/// <summary>
	/// Raised when any of the properties changed.
	/// </summary>
	event EventHandler? Changed;

	#endregion

	#region properties

	/// <summary>
	/// The current state of the connection to the import target.
	/// </summary>
	ConnectionStatus ConnectionStatus { get; }

	/// <summary>
	/// The current run state of the associated runtime, e.g. Running.
	/// </summary>
	RunState RunState { get; }

	#endregion
}