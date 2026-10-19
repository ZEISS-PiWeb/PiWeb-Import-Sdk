#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Immutable;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

namespace SimpleTxtPlugin;

public sealed class ImportFormat : IImportFormat
{
    public IImportGroupFilter CreateImportGroupFilter(ICreateImportGroupFilterContext context)
    {
        return new SimpleTxtImportGroupFilter();
    }

    public IImportParser CreateImportParser(ICreateImportParserContext context)
    {
        return new ImportParser();
    }

    public IImportFormatConfiguration CreateConfiguration(ICreateImportFormatConfigurationContext context)
    {
        return new ImportFormatConfiguration
            {
                SupportsAttributeMapping = true,
                DefaultAttributeMappingConfiguration = new AttributeMappingConfiguration
                {
                    AutoMapping = true,
                    MappingRules = new[] {
                        new MappingRule { AttributeKey = 1, MappingTarget = MappingTarget.Measurement, ValueExpression = "${ Value }" },
                        new MappingRule { AttributeKey = 2101, MappingTarget = MappingTarget.Characteristic, ValueExpression = "${ Nominal }" },
                        new MappingRule { AttributeKey = 2543, MappingTarget = MappingTarget.Characteristic, ValueExpression = "${ XPosition }" },
                        new MappingRule { AttributeKey = 2544, MappingTarget = MappingTarget.Characteristic, ValueExpression = "${ YPosition }" },
                        new MappingRule { AttributeKey = 2545, MappingTarget = MappingTarget.Characteristic, ValueExpression = "${ ZPosition }" }
                    }.ToImmutableList()
                }
            };
    }
}
