#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Generic;
using Zeiss.PiWeb.Sdk.Import.ImportController.Exceptions;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;

namespace Zeiss.PiWeb.Sdk.Import.ImportController;

/// <summary>
/// Responsible for controlling the import pipeline. Provides methods to modify the behavior of the
/// import process.
/// </summary>
public interface IImportController
{
    /// <summary>
    /// Indicates whether rescheduling of import files is supported.
    /// Rescheduling will not be available in all contexts. For example rescheduling is typically not supported
    /// if import files are imported interactively. This property should always be checked before using
    /// <see cref="RescheduleImportFiles"/>. If a file needs to be scheduled, but the operation is not supported, an
    /// import error should be raised. 
    /// </summary>
    bool CanRescheduleImportFiles { get; }

    /// <summary>
    /// Schedules the given import files to being added back into the queue of unprocessed import files after a
    /// wait time. The default wait time is predetermined by the hosting application and may vary on the number
    /// of times an import file was rescheduled previously.
    /// After the import files are added back to the import queue, they will be picked up again by import file grouping
    /// and thus will be processed once more as import.
    /// Only import files of the currently active import group may be rescheduled.
    /// Rescheduling of one or more import files does not remove these files from their current import group.
    /// They will be processed as normal except for not being backed up or deleted at the end.
    /// This is useful to process import files again when for any reason the first processing could not import it
    /// completely or at all.
    /// Rescheduling will not be available in all contexts. For example rescheduling is typically not supported
    /// if import files are imported interactively. Always check <see cref="CanRescheduleImportFiles"/> first.
    /// </summary>
    /// <param name="files">
    /// The import files to reschedule.
    /// </param>
    /// <exception cref="SchedulingException">
    /// Thrown when any of the given import files cannot be rescheduled.
    /// </exception>
    /// <exception cref="SchedulingNotSupportedException">Thrown when rescheduling is not supported.</exception>
    /// <exception cref="ImportControllerException">
    /// Thrown when this import controller is used after the import it belongs to is already finished.
    /// </exception> 
    SchedulingResult RescheduleImportFiles(IEnumerable<IImportFile> files);
}