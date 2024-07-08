#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Specifies the configuration settings that are supported by the import format and default values for them.
/// </summary>
public class ImportFormatConfiguration
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
    public AttributeMappingConfiguration DefaultAttributeMappingConfiguration { get; init; } = AttributeMappingConfiguration.Empty;
}