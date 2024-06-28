#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Immutable;

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents the attribute mapping rules to be used for an import format.
/// </summary>
public class AttributeMappingConfiguration
{
    /// <summary>
    /// An <see cref="AttributeMappingConfiguration"/> instance without any attribute mapping rules.
    /// </summary>
    public static readonly AttributeMappingConfiguration Empty = new AttributeMappingConfiguration();

    /// <summary>
    /// Specifies whether auto mapping should be applied.
    /// The auto mapping functionality maps entity variables of the form Kxxxx automatically
    /// to attributes with the same attribute key without requiring an explicit mapping rule.
    /// Auto mapping for an attribute is only applied if no explicit mapping rule for this attribute exists.
    /// </summary>
    public bool AutoMapping { get; init; } = true;

    /// <summary>
    /// The list of mapping rules. 
    /// </summary>
    public ImmutableList<MappingRule> MappingRules { get; init; } = [];
}