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
using Zeiss.PiWeb.Sdk.Import.ImportFiles;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Responsible for parsing import files and creating inspection plan data, measurement data and additional data
/// which can be imported to the PiWeb database. Each import format provides one parser implementation to deal with
/// groups of import files of this format. A <see cref="IImportGroupFilter"/> instance also associated with the import
/// format determines which import groups consisting of which import files are passed to the import parser for parsing. 
/// </summary>
/// <exception cref="OperationCanceledException">Thrown when the operation is canceled.</exception>
public interface IImportParser
{
	/// <summary>
	/// Creates data to be imported by parsing a given import group.
	/// </summary>
	/// <param name="importGroup">The import group to parse.</param>
	/// <param name="context">Provides information about the import context.</param>
	/// <param name="cancellationToken">A cancellation token to signal cancellation of the import.</param>
	Task<ImportData.ImportData> ParseAsync(
		IImportGroup importGroup,
		IParseContext context,
		CancellationToken cancellationToken = default);
}