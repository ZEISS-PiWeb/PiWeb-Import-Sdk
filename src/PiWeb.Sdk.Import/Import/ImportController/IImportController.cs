#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
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
    /// <see cref="RescheduleImportFiles(System.Collections.Generic.IEnumerable{Zeiss.PiWeb.Sdk.Import.ImportFiles.IImportFile})" />.
    /// If a file needs to be scheduled, but the operation is not supported, an
    /// import error should be raised.
    /// </summary>
    bool CanRescheduleImportFiles { get; }

    /// <summary>
    /// Schedules the given import files to be added back into the queue of unprocessed import files. The given import
    /// files will not be added back immediately but after a hosting application specific wait time (which may also
    /// depend on previous rescheduling operations). During this wait time the given files will not be considered for
    /// import. Afterwarts the files are available again and will be treated as new import files.
    /// Rescheduling of one or more import files does not remove these files from their current import group,
    /// but after the current import finishes, import files will be kept in the import folder (without file backup).
    /// Rescheduled files will not be mentioned in the import history entries of the current import. If all files
    /// of the current import group are rescheduled, no import history entry for the current import is created at all.
    /// Only import files of the currently active import group may be rescheduled.
    /// Note: Rescheduling will not be available in all contexts. For example rescheduling is typically not supported
    /// if import files are imported interactively. Always check <see cref="CanRescheduleImportFiles" /> first.
    /// </summary>
    /// <remarks>
    /// This method is useful when an import file cannot be imported at the current point in time because some
    /// external information (e.g. an already existing measurement) is missing. Rescheduling the import
    /// file will retry the import at a later point in time.
    /// </remarks>
    /// <param name="importFiles">
    /// The import files to reschedule.
    /// </param>
    /// <exception cref="SchedulingException">
    /// Thrown when any of the given import files cannot be rescheduled.
    /// </exception>
    /// <exception cref="SchedulingNotSupportedException">Thrown when rescheduling is not supported.</exception>
    /// <exception cref="ImportControllerException">
    /// Thrown when this import controller is used after the import it belongs to is already finished.
    /// </exception>
    SchedulingResult RescheduleImportFiles(IEnumerable<IImportFile> importFiles);
    
    /// <summary>
    /// Schedules the given import files to be added back into the queue of unprocessed import files. The given import
    /// files will not be added back immediately but after a hosting application specific wait time (which may also
    /// depend on previous rescheduling operations). During this wait time the given files will not be considered for
    /// import. Afterwarts the files are available again and will be treated as new import files.
    /// Rescheduling of one or more import files does not remove these files from their current import group,
    /// but after the current import finishes, import files will be kept in the import folder (without file backup).
    /// The construction of the import history entry of the current import can be specified using the
    /// <paramref name="schedulingSettings"/> parameter.   
    /// Only import files of the currently active import group may be rescheduled.
    /// Note: Rescheduling will not be available in all contexts. For example rescheduling is typically not supported
    /// if import files are imported interactively. Always check <see cref="CanRescheduleImportFiles" /> first.
    /// </summary>
    /// <remarks>
    /// This method is useful when an import file cannot be imported at the current point in time because some
    /// external information (e.g. an already existing measurement) is missing. Rescheduling the import
    /// file will retry the import at a later point in time. Using the <paramref name="schedulingSettings"/> parameter,
    /// it is possible to specify whether each import attempt results in an import history entry or only the last
    /// attempt.
    /// </remarks>
    /// <param name="importFiles">
    /// The import files to reschedule.
    /// </param>
    /// <param name="schedulingSettings"></param>
    /// <exception cref="SchedulingException">
    /// Thrown when any of the given import files cannot be rescheduled.
    /// </exception>
    /// <exception cref="SchedulingNotSupportedException">Thrown when rescheduling is not supported.</exception>
    /// <exception cref="ImportControllerException">
    /// Thrown when this import controller is used after the import it belongs to is already finished.
    /// </exception>
    SchedulingResult RescheduleImportFiles(IEnumerable<IImportFile> importFiles, SchedulingSettings schedulingSettings);
}