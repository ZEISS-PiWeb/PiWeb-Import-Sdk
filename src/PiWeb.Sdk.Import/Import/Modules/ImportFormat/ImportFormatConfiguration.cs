#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// <inheritdoc />
/// This implementation allows path rules and attribute mapping and starts with an empty mapping rules table.
/// </summary>
public class ImportFormatConfiguration : IImportFormatConfiguration
{
    /// <summary>
    /// Specifies whether the import format supports the usage and configuration of path rules. 
    /// </summary>
    public bool SupportsPathRules { get; init; } = true;

    /// <summary>
    /// Specifies whether the import format supports the usage and configuration of attribute mapping. 
    /// </summary>
    public bool SupportsAttributeMapping { get; init; } = true;
    
    /// <summary>
    /// The default attribute mapping to use when attribute mapping is supported.
    /// </summary>
    public AttributeMappingConfiguration DefaultAttributeMappingConfiguration { get; init; } =
        AttributeMappingConfiguration.Empty;
}