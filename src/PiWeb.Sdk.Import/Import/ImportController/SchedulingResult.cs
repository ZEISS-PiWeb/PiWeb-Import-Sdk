#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Sdk.Import.ImportController;

/// <summary>
/// Represents the result of a scheduling operation.
/// </summary>
public class SchedulingResult
{
    /// <summary>
    /// Indicates the date and time the scheduled file will be added back to the import queue if this information
    /// is available. Otherwise, this property is null.
    /// </summary>
    public DateTimeOffset? DueTime { get; init; }
}