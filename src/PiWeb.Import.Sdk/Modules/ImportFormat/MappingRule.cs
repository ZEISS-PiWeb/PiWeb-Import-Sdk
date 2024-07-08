#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents a rule specifying the mapping for an attribute of a part, characteristic, measurement or measured value.
/// </summary>
public class MappingRule
{
    /// <summary>
    /// The entities this rule applies to.
    /// </summary>
    public required MappingTarget MappingTarget { get; init; }

    /// <summary>
    /// The key of the attribute this rule defines a mapping for.
    /// </summary>
    public required ushort AttributeKey { get; init; }
    
    /// <summary>
    /// An expression that defines the value to be applied to the attribute.
    /// </summary>
    public required string ValueExpression { get; init; }
    
    /// <summary>
    /// The name of the culture to be used for the evaluation of the value expression, e.g. "en-US" or "de".
    /// If the name is empty or does not belong to any known culture, the invariant culture will be used.
    /// </summary>
    public string MappingCultureName { get; init; } = "";

    /// <summary>
    /// The type of comparison values to be used when mapping to a catalog attribute.
    /// </summary>
    public CatalogMappingType CatalogMappingType { get; init; } = CatalogMappingType.Index;
    
    /// <summary>
    /// The catalog column that provides the comparison values when <see cref="CatalogMappingType"/>
    /// is set to <see cref="CatalogMappingType.Column"/>.
    /// </summary>
    public ushort CatalogColumnKey { get; init; } = ushort.MinValue;

}
