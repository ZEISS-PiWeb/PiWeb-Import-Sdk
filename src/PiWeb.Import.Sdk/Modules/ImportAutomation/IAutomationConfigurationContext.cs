#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

using System;
using Environment;
using ImportPlan;
using Logging;
using PropertyStorage;

/// <summary>
/// Represents the context of an automation configuration for an import automation.
/// </summary>
public interface IAutomationConfigurationContext
{
	#region events

	/// <summary>
	/// Raised when any of the properties changed.
	/// </summary>
	public event EventHandler? SetupChanged;

	/// <summary>
	/// Raised when any of the properties changed.
	/// </summary>
	public event EventHandler? StatusChanged;

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
	/// The import plan execution specific properties such as the lifetime.
	/// </summary>
	Execution Execution { get; }

	/// <summary>
	/// The current state of the connection to the import target.
	/// </summary>
	ConnectionStatus ConnectionStatus { get; }

	/// <summary>
	/// The current run state of the associated runtime, e.g. Running.
	/// </summary>
	RunState RunState { get; }

	/// <summary>
	/// The property storage for an automation module of the current import plan.
	/// </summary>
	public IPropertyStorage PropertyStorage { get; }

	/// <summary>
	/// Contains information about the environment a plugin is hosted in.
	/// </summary>
	IEnvironmentInfo EnvironmentInfo { get; }

	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the hosting application.
	/// </summary>
	ILogger Logger { get; }

	#endregion
}