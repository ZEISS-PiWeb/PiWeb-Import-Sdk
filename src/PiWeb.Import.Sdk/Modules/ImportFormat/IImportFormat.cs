#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.Threading.Tasks;
using ImportData = ImportData.ImportData;

#endregion

/// <summary>
/// Represents a custom import format provided as part of a plugin. Custom import formats provide a way to identify and group relevant
/// files, parse their data and create a data image that can be uploaded to PiWeb Server.
/// </summary>
public interface IImportFormat
{
    #region methods

    /// <summary>
    /// Decides what should be done with the given <paramref name="importGroup"/>.
    /// See <see cref="ImportAction"/> for more information.
    /// </summary>
    /// <param name="importGroup">The potential import group containing the primary file to analyze.</param>
    /// <param name="context">Provides context information such as other files in the import folder.</param>
    ValueTask<ImportAction> DecideImportAction(IImportGroup importGroup, IGroupContext context);

    /// <summary>
    /// Creates data to be imported by parsing a given import group.
    /// </summary>
    /// <param name="importGroup">The import group to parse.</param>
    /// <param name="context">Provides information about the import context.</param>
    Task<ImportData> ParseImportData(IImportGroup importGroup, IParseContext context);

    /// <summary>
    /// Gets the import format configuration specifying supported import format configuration settings.
    /// </summary>
    /// <returns>The import format configuration.</returns>
    ImportFormatConfiguration GetConfiguration();

    #endregion
}