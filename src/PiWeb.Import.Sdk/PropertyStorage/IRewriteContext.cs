#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.PropertyStorage;

using System;
using Environment;
using Logging;

/// <summary>
/// Represents the context of a property storage rewrite operation.
/// </summary>
public interface IRewriteContext
{
	/// <summary>
	/// Specifies the reason for rewriting a property storage.
	/// </summary>
	public RewriteReason RewriteReason { get; }

	/// <summary>
	/// The id of the associated import plan.
	/// </summary>
	Guid ImportPlanId { get; }

	/// <summary>
	/// Contains information about the environment a plugin is hosted in.
	/// </summary>
	IEnvironmentInfo EnvironmentInfo { get; }

	/// <summary>
	/// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the hosting application.
	/// </summary>
	ILogger Logger { get; }
}