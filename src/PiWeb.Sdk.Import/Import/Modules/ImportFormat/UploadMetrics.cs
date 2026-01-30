#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
///     Represents information about the number of various operations made during upload.
/// </summary>
public record class UploadMetrics
{
    /// <summary>
    /// The number of entities created by the upload.
    /// </summary>
    public uint Created { get; set; }

    /// <summary>
    /// The number of entities updated by the upload.
    /// </summary>
    public uint Updated { get; set; }

    /// <summary>
    /// The number of entities deleted by the upload.
    /// </summary>
    public uint Deleted { get; set; }

    /// <summary>
    /// The number of additional data items created by the upload.
    /// </summary>
    public uint AdditionalDataItemsCreated { get; set; }

    /// <summary>
    /// The number of additional data items updated by the upload.
    /// </summary>
    public uint AdditionalDataItemsUpdated { get; set; }

    /// <summary>
    /// The number of additional data items deleted by the upload.
    /// </summary>
    public uint AdditionalDataItemsDeleted { get; set; }
}