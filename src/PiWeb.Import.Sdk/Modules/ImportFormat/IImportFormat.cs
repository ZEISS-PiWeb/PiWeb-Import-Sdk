#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.Collections.Generic;

#endregion

/// <summary>
/// Represents a custom import format provided as part of a plugin. Custom import formats provide a way to identify and group relevant
/// files, parse their data and create a data image that can be uploaded to PiWeb Server.
/// </summary>
public interface IImportFormat
{
    #region methods

    /// <summary>
    /// Builds an import group if the <see cref="IImportFormat"/> recognizes the <paramref name="primaryFileSource"/> and other
    /// <see cref="IFileSource"/>s provided by the <paramref name="context"/>.
    /// </summary>
    /// <param name="primaryFileSource">The file to analyze and built a group of.</param>
    /// <param name="context">Provides context information such as other files in the import folder.</param>
    /// <returns>An <see cref="ImportGroup"/> defining the files identified; or <c>null</c> otherwise.</returns>
    ImportGroup? BuildGroup(IPrimaryFileSource primaryFileSource, IGroupContext context);

    #endregion

    #region properties

    /// <summary>
    /// The format identifier.
    /// </summary>
    string Identifier { get; }

    /// <summary>
    /// The short display name of the import format.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// The associated file extensions. The information is displayed in the import formats dialog.
    /// </summary>
    IReadOnlyCollection<string> StandardFileExtensions { get; }

    /// <summary>
    /// The display type of the import formats associated files. E.g. binary formats will not show a preview.
    /// </summary>
    SourceDisplayType SourceDisplay { get; }

    #endregion
}