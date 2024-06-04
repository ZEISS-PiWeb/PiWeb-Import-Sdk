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
/// Represents a set of files that will be imported, discarded or deferred depending on <see cref="State"/>.
/// </summary>
public sealed record ImportGroup
{
    #region properties

    /// <summary>
    /// The primary file with which the grouping was started. This file will not be offered to other import formats.
    /// </summary>
    public required IPrimaryFileSource PrimaryFile { get; set; }

    /// <summary>
    /// The list of files recognized by the import format including <see cref="PrimaryFile"/>.
    /// These files will not be offered to other import formats.
    /// </summary>
    public ImmutableList<IFileSource> Files { get; set; } = ImmutableList<IFileSource>.Empty;

    /// <summary>
    /// Describes how the import should handle this <see cref="ImportGroup"/>. See <see cref="State"/> for more information.
    /// </summary>
    public required State State { get; set; }

    #endregion
}