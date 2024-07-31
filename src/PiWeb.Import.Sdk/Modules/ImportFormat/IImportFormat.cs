#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using Zeiss.PiWeb.Import.Sdk.ImportFiles;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents a custom import format provided as part of a plugin. Custom import formats provide a way to identify and group relevant
/// files, parse their data and create a data image that can be uploaded to PiWeb Server.
/// </summary>
public interface IImportFormat
{
    #region methods

    /// <summary>
    /// Gets the import format configuration. This configuration specifies which import format settings
    /// are supported by this import format and which default values should be used for these settings.
    /// </summary>
    /// <returns>The import format configuration.</returns>
    IImportFormatConfiguration CreateConfiguration(ICreateImportFormatConfigurationContext context)
    {
        return new StandardImportFormatConfiguration();
    }

    /// <summary>
    /// Creates an import group filter associated with this import format. The import group filter is responsible for
    /// filtering and completing import groups consisting of import files that should be handled by this import format.  
    /// </summary>
    /// <param name="context">Provides context information.</param>
    /// <returns>The created import group filter.</returns>
    IImportGroupFilter CreateImportGroupFilter(ICreateImportGroupFilterContext context);

    /// <summary>
    /// Creates an import parser associated with this import format. The import parser is responsible for reading
    /// import files and converting their contents to an import data object that can subsequently be written to
    /// the piweb database.
    /// </summary>
    /// <param name="context">Provides context information.</param>
    /// <returns>The created import parser.</returns>
    IImportParser CreateImportParser(ICreateImportParserContext context);

    #endregion
}