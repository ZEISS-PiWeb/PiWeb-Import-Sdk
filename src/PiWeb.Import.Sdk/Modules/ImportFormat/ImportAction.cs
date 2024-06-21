#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents the result of <see cref="IImportFormat.DecideImportAction"/> and defines how the <see cref="IImportGroup"/> should be handled by
/// PiWeb Auto Importer.
/// </summary>
public enum ImportAction
{
    /// <summary>
    /// <see cref="IImportGroup.PrimaryFile"/> has an unknown format and <see cref="IImportGroup"/> should be offered to another
    /// <see cref="IImportFormat"/>.
    /// </summary>
    Pass,

    /// <summary>
    /// The <see cref="IImportGroup"/> contains all relevant information and can be imported at once.
    /// </summary>
    Import,

    /// <summary>
    /// <see cref="IImportGroup.PrimaryFile"/> was recognized but the <see cref="IImportGroup"/> should be discarded, e.g. because a file
    /// is corrupted. The files in <see cref="IImportGroup.AllFiles"/> will not be offered to another import format and will be removed
    /// from the import folder.
    /// </summary>
    Discard,

    /// <summary>
    /// <see cref="IImportGroup.PrimaryFile"/> was recognized and the <see cref="IImportGroup"/> contains enough information to be imported
    /// at once, but can also wait for additional information that might appear later.
    /// So the <see cref="IImportGroup"/> should only be imported if the retention period of the files in
    /// <see cref="IImportGroup.AllFiles"/> is expired.
    /// If the retention period is not expired, the files in <see cref="IImportGroup.AllFiles"/> will not be imported in the ongoing import
    /// run but will be available again in the next import run. 
    /// </summary>
    RetryOrImport,

    /// <summary>
    /// <see cref="IImportGroup.PrimaryFile"/> was recognized but the <see cref="IImportGroup"/> can not be imported. That status might be
    /// different in the next import run. So the <see cref="IImportGroup"/> should only be discarded if the retention period of the files
    /// in <see cref="IImportGroup.AllFiles"/> is expired.
    /// If the retention period is not expired, the files in <see cref="IImportGroup.AllFiles"/> will not be discarded in the ongoing import
    /// run but will be available again in the next import run. 
    /// </summary>
    RetryOrDiscard
}