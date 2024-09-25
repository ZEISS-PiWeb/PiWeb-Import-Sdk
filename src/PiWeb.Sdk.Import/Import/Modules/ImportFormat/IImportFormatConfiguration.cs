#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Represents configuration settings of an import format.
/// </summary>
public interface IImportFormatConfiguration
{
    /// <summary>
    /// Specifies whether the import format supports the usage and configuration of path rules. 
    /// </summary>
    bool SupportsPathRules { get; init; }

    /// <summary>
    /// Specifies whether the import format supports the usage and configuration of attribute mapping. 
    /// </summary>
    bool SupportsAttributeMapping { get; init; }

    /// <summary>
    /// The default attribute mapping to use when attribute mapping is supported.
    /// </summary>
    AttributeMappingConfiguration DefaultAttributeMappingConfiguration { get; init; }
}