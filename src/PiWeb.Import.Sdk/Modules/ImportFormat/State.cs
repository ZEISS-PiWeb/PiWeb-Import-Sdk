#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

/// <summary>
/// Defines the <see cref="ImportGroup"/>s state.
/// </summary>
public enum State
{
    /// <summary>
    /// The import group is complete. No additional files will be added to this import group even if new files appear in the import source.
    /// Completed import groups are usually imported immediately.
    /// </summary>
    Complete,

    /// <summary>
    /// The import group is ready. All files necessary for an import are present but additional files may still be added when new files
    /// appear in the import source. Depending on configuration, ready import groups are usually only imported after a delay to make sure
    /// no additional files show up.
    /// </summary>
    Ready
}