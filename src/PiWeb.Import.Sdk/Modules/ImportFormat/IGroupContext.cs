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
/// Holds contextual information that might be required during the grouping of files for the import.
/// </summary>
public interface IGroupContext
{
    #region properties

    /// <summary>
    /// The list of available files that have not been associated with any import format yet.
    /// </summary>
    /// <remarks>
    /// This list does not contain the primary file source that is a parameter of <see cref="IImportFormat.DecideImportAction"/>.
    /// </remarks>
    IEnumerable<IFileSource> Files { get; }

    #endregion

    #region methods

    /// <summary>
    /// Gets all <see cref="IFileSource"/>s within <see cref="Files"/> that match the <paramref name="wildcardPattern"/>
    /// and that are in the same folder as the primary file source (<see cref="IImportFormat.DecideImportAction"/>).
    /// </summary>
    /// <remarks>Supports the following wildcards: '*' and '?'. It ignores the file casing.</remarks>
    /// <param name="wildcardPattern">The wildcard pattern to match.</param>
    /// <returns>All matching <see cref="IFileSource"/>s</returns>
    IEnumerable<IFileSource> FindFilesLike(string wildcardPattern);

    #endregion
}