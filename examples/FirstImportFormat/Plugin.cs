#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Import;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

namespace Zeiss.FirstImportFormat;

public class Plugin : IPlugin
{
    /// <inheritdoc />
    public IImportFormat CreateImportFormat(ICreateImportFormatContext context)
    {
        return new FirstImportFormat.ImportFormat();
    }
}
