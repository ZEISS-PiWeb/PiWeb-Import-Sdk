﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using Zeiss.PiWeb.Sdk.Import;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

#endregion

namespace Zeiss.FirstImportFormat;

public class Plugin : IPlugin
{
    /// <inheritdoc />
    public IImportFormat CreateImportFormat(ICreateImportFormatContext context)
    {
        return new FirstImportFormat.ImportFormat();
    }
}