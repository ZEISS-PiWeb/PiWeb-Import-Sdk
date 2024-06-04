#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Represents the file with which <see cref="IImportFormat.BuildGroup"/> was called.
/// It is used to distinguish other files from that primary file.
/// </summary>
public interface IPrimaryFileSource : IFileSource
{
}