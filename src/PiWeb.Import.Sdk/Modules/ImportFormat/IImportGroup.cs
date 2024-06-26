#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Import.Sdk.FileSources;

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.Collections.Generic;

#endregion

/// <summary>
/// Represents a set of files that will be imported, discarded or deferred depending on <see cref="ImportAction"/>.
/// </summary>
public interface IImportGroup
{
    #region properties

    /// <summary>
    /// The primary file with which the grouping was started. This file has to be recognized by the format.
    /// </summary>
    IFileSource PrimaryFile { get; }

    /// <summary>
    /// The list of additional files recognized by the import format and added using <see cref="AddFile"/>.
    /// The list does not include <see cref="PrimaryFile"/>. These files will not be offered to other import formats.
    /// </summary>
    ICollection<IFileSource> AdditionalFiles { get; }

    /// <summary>
    /// The list of files recognized by the import format including <see cref="PrimaryFile"/>.
    /// These files will not be offered to other import formats.
    /// </summary>
    ICollection<IFileSource> AllFiles { get; }

    /// <summary>
    /// Adds the <paramref name="fileSource"/> to this <see cref="IImportGroup"/>. 
    /// These files will not be offered to other import formats. 
    /// </summary>
    void AddFile(IFileSource fileSource);

    /// <summary>
    /// Adds the <paramref name="fileSources"/> to this <see cref="IImportGroup"/>. 
    /// These files will not be offered to other import formats. 
    /// </summary>
    void AddFiles(IEnumerable<IFileSource> fileSources);

    /// <summary>
    /// Removes the <paramref name="fileSource"/> from this <see cref="IImportGroup"/>. 
    /// The file will be offered to other import formats. It's not possible to remove <see cref="PrimaryFile"/>.
    /// </summary>
    void RemoveFile(IFileSource fileSource);

    #endregion
}