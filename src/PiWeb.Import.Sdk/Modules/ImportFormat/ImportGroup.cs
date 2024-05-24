#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#region usings

using System.Collections.Immutable;

#endregion

/// <summary>
/// Represents a set of files that are imported as one group.
/// </summary>
public sealed record ImportGroup
{
    #region properties

    /// <summary>
    /// Gets the list of files in the import group. These files will not be considered by other import formats.
    /// </summary>
    public ImmutableList<IFileSource> Files { get; set; } = ImmutableList<IFileSource>.Empty;

    /// <summary>
    /// Describes the state of this <see cref="ImportGroup"/>.
    /// </summary>
    public State State { get; set; }

    #endregion
}