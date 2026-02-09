#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportController;

/// <summary>
/// Additional settings to determine the behavior of scheduling operations.
/// </summary>
public record SchedulingSettings
{
    /// <summary>
    /// Specifies whether the rescheduled files should still be reported in the import history entry of the
    /// current import. If none of the import files of the current import group is left to be reported in the import
    /// history, no import history entry for the current import will be created at all. 
    /// </summary>
    public bool ReportInCurrentImportHistory { get; set; } = false;
}