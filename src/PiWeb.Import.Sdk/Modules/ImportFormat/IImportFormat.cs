﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.Collections.Generic;
using ImportData = ImportData.ImportData;

#endregion

/// <summary>
/// Represents a custom import format provided as part of a plugin. Custom import formats provide a way to identify and group relevant
/// files, parse their data and create a data image that can be uploaded to PiWeb Server.
/// </summary>
public interface IImportFormat
{
    #region methods

    /// <summary>
    /// Decides what should be done with the given <paramref name="importGroup"/>. See <see cref="ImportAction"/> for more information.
    /// </summary>
    /// <param name="importGroup">The potential import group containing the primary file to analyze.</param>
    /// <param name="context">Provides context information such as other files in the import folder.</param>
    ImportAction DecideImportAction(IImportGroup importGroup, IGroupContext context);

    /// <summary>
    /// Creates data to be imported by parsing a given import group.
    /// </summary>
    /// <param name="importGroup">The import group to parse.</param>
    /// <param name="context">Provides information about the import context.</param>
    ImportData ParseImportData(IImportGroup importGroup, IParseContext context);

    #endregion

    #region properties

    /// <summary>
    /// The associated file extensions of this format. This information is used to create masks for file
    /// selection dialogs. E.g. [".txt", ".csv"].
    /// </summary>
    IReadOnlyCollection<string> StandardFileExtensions { get; }

    /// <summary>
    /// The display type of the import formats associated files. E.g. binary formats will not show a preview.
    /// </summary>
    SourceDisplayType SourceDisplay { get; }

    #endregion
}