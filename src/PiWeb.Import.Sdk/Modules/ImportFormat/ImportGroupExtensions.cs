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
/// Provides extension methods for <see cref="ImportGroup"/>.
/// </summary>
public static class ImportGroupExtensions
{
    #region methods

    /// <summary>
    /// Creates an <see cref="ImportGroup"/> that is complete and can be imported at once.
    /// </summary>
    /// <param name="associatedFile">The file to be imported.</param>
    public static ImportGroup CreateCompleteImportGroup(this IFileSource associatedFile)
    {
        var importGroup = new ImportGroup();
        importGroup.Files = importGroup.Files.Add(associatedFile);
        importGroup.State = State.Complete;

        return importGroup;
    }

    /// <summary>
    /// Creates an <see cref="ImportGroup"/> that is complete and can be imported at once.
    /// </summary>
    /// <param name="associatedFiles">The files to be imported together.</param>
    public static ImportGroup CreateCompleteImportGroup(this IEnumerable<IFileSource> associatedFiles)
    {
        var importGroup = new ImportGroup();
        importGroup.Files = importGroup.Files.AddRange(associatedFiles);
        importGroup.State = State.Complete;

        return importGroup;
    }

    /// <summary>
    /// Creates an <see cref="ImportGroup"/> that is ready but misses some information. It can be imported when the missing information
    /// does not appear after the designated waiting time.
    /// </summary>
    /// <param name="associatedFile">The file to be imported.</param>
    public static ImportGroup CreateReadyImportGroup(this IFileSource associatedFile)
    {
        var importGroup = new ImportGroup();
        importGroup.Files = importGroup.Files.Add(associatedFile);
        importGroup.State = State.Ready;

        return importGroup;
    }

    /// <summary>
    /// Creates an <see cref="ImportGroup"/> that is ready but misses some information. It can be imported when the missing information
    /// does not appear after the designated waiting time.
    /// </summary>
    /// <param name="associatedFiles">The files to be imported together.</param>
    public static ImportGroup CreateReadyImportGroup(this IEnumerable<IFileSource> associatedFiles)
    {
        var importGroup = new ImportGroup();
        importGroup.Files = importGroup.Files.AddRange(associatedFiles);
        importGroup.State = State.Ready;

        return importGroup;
    }

    #endregion
}