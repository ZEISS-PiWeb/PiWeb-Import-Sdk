#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Generic;

namespace Zeiss.PiWeb.Import.Sdk.ImportFiles;

/// <summary>
/// Represents a set of import files that will be imported or discarded together. For single file import formats,
/// import groups typically consist of a single import file, the primary file. Conversely, for multi file import
/// formats, a group typically consists of the original primary file and any number of additional files that are also
/// required for import.
/// An import group always starts as an initial import group only consisting of the primary import file it was
/// created from. Additional import files are then added as necessary during import group filtering.
/// </summary>
public interface IImportGroup
{
    #region properties

    /// <summary>
    /// The primary file with which the grouping was started. This file has to be recognized by the format.
    /// </summary>
    IImportFile PrimaryFile { get; }

    /// <summary>
    /// The additional import files added to the group.
    /// </summary>
    IReadOnlyList<IImportFile> AdditionalFiles { get; }

    /// <summary>
    /// Adds the given import file to this import group. 
    /// </summary>
    void AddFile(IImportFile importFile);

    /// <summary>
    /// Adds the given import files to this import group. 
    /// </summary>
    void AddFiles(IEnumerable<IImportFile> importFiles);

    /// <summary>
    /// Removes the given import file from this import group.
    /// </summary>
    void RemoveFile(IImportFile importFile);

    #endregion
}