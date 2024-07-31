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
    /// The list of additional files recognized by the import format and added using <see cref="AddFile"/>.
    /// The list does not include <see cref="PrimaryFile"/>. These files will not be offered to other import formats.
    /// </summary>
    IEnumerable<IImportFile> AdditionalFiles { get; }

    /// <summary>
    /// The list of files recognized by the import format including <see cref="PrimaryFile"/>.
    /// These files will not be offered to other import formats.
    /// </summary>
    IEnumerable<IImportFile> AllFiles { get; }

    /// <summary>
    /// Adds the <paramref name="importFile"/> to this <see cref="IImportGroup"/>. 
    /// These files will not be offered to other import formats. 
    /// </summary>
    void AddFile(IImportFile importFile);

    /// <summary>
    /// Adds the <paramref name="fileSources"/> to this <see cref="IImportGroup"/>. 
    /// These files will not be offered to other import formats. 
    /// </summary>
    void AddFiles(IEnumerable<IImportFile> fileSources);

    /// <summary>
    /// Removes the <paramref name="importFile"/> from this <see cref="IImportGroup"/>. 
    /// The file will be offered to other import formats. It's not possible to remove <see cref="PrimaryFile"/>.
    /// </summary>
    void RemoveFile(IImportFile importFile);

    #endregion
}