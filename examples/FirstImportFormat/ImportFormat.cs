#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

#endregion

namespace Zeiss.FirstImportFormat;

public sealed class ImportFormat : IImportFormat
{
    public IImportGroupFilter CreateImportGroupFilter(ICreateImportGroupFilterContext context)
    {
        return new SimpleTxtImportGroupFilter();
    }

    public IImportParser CreateImportParser(ICreateImportParserContext context)
    {
        return new SimpleTxtImportParser();
    }
}
