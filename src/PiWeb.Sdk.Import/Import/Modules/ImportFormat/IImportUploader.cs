#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.ImportData;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Responsible for uploading parse results to the PiWeb database. An Import format may provide a custom
/// import uploader implementation or opt to use the built-in default import uploader. Import formats usually
/// use the default import uploader. Custom import uploaders are only necessary in very specific cases.  
/// </summary>
/// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
public interface IImportUploader
{
	/// <summary>
	/// Uploads the given import data to the PiWeb database.
	/// </summary>
	/// <param name="importData">The import data to upload.</param>
	/// <param name="context">Provides information about the import context.</param>
	/// <param name="cancellationToken">A cancellation token to signal cancellation of the import.</param>
	/// <returns>
	/// The result of the upload. This includes information about the number of operations on entities and the path
	/// of the part this upload should be associated with.
	/// </returns>
	/// <exception cref="UploadException">Thrown when the data upload fails.</exception>
	Task<UploadResult> UploadAsync(
		ImportData.ImportData importData,
		IUploadContext context,
		CancellationToken cancellationToken = default);
}