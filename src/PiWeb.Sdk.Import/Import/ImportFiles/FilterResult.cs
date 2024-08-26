#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportFiles;

/// <summary>
/// Represents possible results of import group filtering. The result determines how a given import group is handled
/// by the import pipeline. 
/// </summary>
public enum FilterResult
{
    /// <summary>
    /// Signals that a given import group should not be handled by the import format associated with the import group
    /// filter. This causes the import pipeline to pass the group down the current filtering chain to the import group
    /// filter of the import format with the next higher priority. If none of the import filters of the filtering chain
    /// provide a filter result other than <see cref="FilterResult.None"/>, the import group will be treated as unknown
    /// import format. The import files of such a group are still available to be picked up as dependencies of other
    /// import groups, however, import files not picked up after some time will be discarded eventually.  
    /// </summary>
    None,

    /// <summary>
    /// Signals that the import group belongs to the import format associated with the import group filter and is ready
    /// to be imported. The import group is removed from the current filtering chain and imported at the next
    /// opportunity. Use this filter result when there are no missing optional dependencies that might appear at a later
    /// point in time. When there are optional dependencies still missing, use <see cref="RetryOrImport"/> instead.  
    /// </summary>
    Import,

    /// <summary>
    /// Signals that the import group belongs to the import format associated with the import group filter but is
    /// permanently defect in some way. The import group is removed from the current filtering chain and deleted at the
    /// next opportunity. Use this filter result when the defect is not likely to be fixed by waiting longer. When the
    /// defect is temporary or there are missing required dependencies, use <see cref="RetryOrDiscard"/> instead.
    /// </summary>
    Discard,

    /// <summary>
    /// Signals that the import group belongs to the import format associated with the import group filter, is ready
    /// to be imported, but it is still missing optional dependencies. The import group is removed from the current
    /// filtering chain but not imported immediately. After some time, this import group will be filtered again. When
    /// an import group is repeatedly filtered as <see cref="RetryOrImport"/> without any changes over a period of time,
    /// it will be imported without satisfying its optional dependencies eventually. 
    /// </summary>
    RetryOrImport,

    /// <summary>
    /// Signals that the import group belongs to the import format associated with the import group filter but is
    /// missing required dependencies or has a temporary defect in some way. The import group is removed from the
    /// current filtering chain but not deleted immediately. After some time, this import group will be filtered again.
    /// When an import group is repeatedly filtered as <see cref="RetryOrDiscard"/> without any changes over a period
    /// of time, it will be discarded eventually. 
    /// </summary>
    RetryOrDiscard
}