#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Common.Logging;
using Zeiss.PiWeb.Sdk.Common.PropertyStorage;
using Zeiss.PiWeb.Sdk.Import.ImportPlan;
using Zeiss.PiWeb.Sdk.Import.ImportSourceDescription;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

/// <summary>
/// Represents the context for creating a configuration for an import automation.
/// </summary>
public interface ICreateAutomationConfigurationContext
{
	#region properties

	/// <summary>
	/// The setup of the associated import plan.
	/// </summary>
	IImportPlanSetup ImportPlanSetup { get; }

	/// <summary>
	/// The status of the associated import plan.
	/// </summary>
	IImportPlanStatus ImportPlanStatus { get; }

	/// <summary>
	/// The property storage for an automation module of the current import plan.
	/// </summary>
	IPropertyStorage PropertyStorage { get; }
	
	/// <summary>
	/// The description of the import source.
	/// </summary>
	IImportSourceDescription ImportSourceDescription { get; }

	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the hosting application.
	/// </summary>
	ILogger Logger { get; }

	#endregion
}