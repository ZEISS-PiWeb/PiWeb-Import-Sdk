#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// The file format of the data file(s) of an <see cref="IImportFormat"/>.
/// </summary>
public enum SourceDisplayType
{
    /// <summary>
    /// This file will be shown as a text file in the configure import formats dialog.
    /// </summary>
    Text,

    /// <summary>
    /// This file will be handled as a binary file without preview in the configure import formats dialog.
    /// </summary>
    Binary
}