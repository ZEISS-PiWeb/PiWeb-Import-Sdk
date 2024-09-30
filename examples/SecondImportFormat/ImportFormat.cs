#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

namespace Zeiss.SecondImportFormat;

public sealed class ImportFormat : IImportFormat
{
    public IImportGroupFilter CreateImportGroupFilter(ICreateImportGroupFilterContext context)
    {
        return new ImportGroupFilter();
    }

    public IImportParser CreateImportParser(ICreateImportParserContext context)
    {
        return new ImportParser();
    }

    public IImportFormatConfiguration CreateConfiguration(ICreateImportFormatConfigurationContext context)
    {
        return new ImportFormatConfiguration
        {
            SupportsPathRules = true,
            SupportsAttributeMapping = true,
            DefaultAttributeMappingConfiguration = new AttributeMappingConfiguration
            {
                MappingRules =
                [
                    new MappingRule
                    {
                        MappingTarget = MappingTarget.MeasuredValue,
                        AttributeKey = 1, // Measured Value
                        ValueExpression = "$Value"
                    },
                    new MappingRule
                    {
                        MappingTarget = MappingTarget.Measurement,
                        AttributeKey = 4, // Time
                        ValueExpression = "$Date",
                        MappingCultureName = "de-DE"
                    },
                    new MappingRule
                    {
                        MappingTarget = MappingTarget.Measurement,
                        AttributeKey = 9, // Text
                        ValueExpression = "$Text"
                    }
                ]
            }
        };
    }
}
