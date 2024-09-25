#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using Zeiss.PiWeb.Sdk.Common.Logging;
using Zeiss.PiWeb.Sdk.Common.PropertyStorage;
using Zeiss.PiWeb.Sdk.Import.ImportPlan;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

/// <summary>
/// Represents the context of an import runner provided by the hosting application.
/// </summary>
public interface ICreateImportRunnerContext
{
	#region properties

	/// <summary>
	/// The id of the associated import plan.
	/// </summary>
	Guid ImportPlanId { get; }

	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
	/// hosting application.
	/// </summary>
	ILogger Logger { get; }

	/// <summary>
	/// The configured import target.
	/// </summary>
	ImportTarget ImportTarget { get; }

	/// <summary>
	/// A reader for th configuration property storage.
	/// </summary>
	public IPropertyReader PropertyReader { get; }

	/// <summary>
	/// A service that can be used to set the current activity of an import runner while it is running. It
	/// can also be used to post activity events.
	/// </summary>
	public IActivityService ActivityService { get; }

	#endregion
}