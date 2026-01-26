#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Represents the result of a data upload.
/// </summary>
public class UploadResult
{
    /// <summary>
    /// The path of the domain part in RoundtripString format. This should be the most specific part that envelops
    /// all changes to the PiWeb database made by the upload. Import history entries of this upload will be attached to
    /// this part.
    /// If this is empty or not a valid part path, the import target part will be used as domain part.  
    /// </summary>
    public string DomainPartPath { get; set; } = string.Empty;

    /// <summary>
    /// Information about the number of various operations on parts made during the upload. This information is
    /// used to create history entries for the import.
    /// </summary>
    public UploadMetrics PartMetrics { get; set; } = new UploadMetrics();

    /// <summary>
    /// Information about the number of various operations on characteristics made during the upload. This information
    /// is used to create history entries for the import.
    /// </summary>
    public UploadMetrics CharacteristicMetrics { get; set; } = new UploadMetrics();

    /// <summary>
    /// Information about the number of various operations on measurements made during the upload. This information is
    /// used to create history entries for the import.
    /// </summary>
    public UploadMetrics MeasurementMetrics { get; set; } = new UploadMetrics();

    /// <summary>
    /// Information about the number of various operations on measured values made during the upload. This information
    /// is used to create history entries for the import.
    /// </summary>
    public UploadMetrics ValueMetrics { get; set; } = new UploadMetrics();
}