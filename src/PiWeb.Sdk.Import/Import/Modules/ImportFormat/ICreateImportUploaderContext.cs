#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using Zeiss.PiWeb.Sdk.Common.Logging;
using Zeiss.PiWeb.Sdk.Import.ImportPlan;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Represents the context for the creation of <see cref="IImportUploader"/> instances. This context will be
/// provided by the hosting application.
/// </summary>
public interface ICreateCustomImportUploaderContext
{
    #region properties

    /// <summary>
    /// The configured import target.
    /// </summary>
    ImportTarget ImportTarget { get; }
	
    /// <summary>
    /// The id of the currently executing import plan.
    /// </summary>
    Guid ImportPlanId { get; }
    
    /// <summary>
    /// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
    /// hosting application.
    /// </summary>
    ILogger Logger { get; }

    #endregion
}