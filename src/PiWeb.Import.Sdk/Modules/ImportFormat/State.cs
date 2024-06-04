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
    /// The import group is complete and should be imported at once.
    /// </summary>
    Valid,
    
    /// <summary>
    /// Import files were recognized and are ready to be imported but there is information missing.
    /// This information might be available later. So the files will not be offered to another import format in the ongoing import run
    /// and will be available in the next import run again. 
    /// If the retention period of one of the files expires then the group is imported with the available information.
    /// </summary>
    ValidDelay,

    /// <summary>
    /// Import files were recognized but there is e.g. information missing. The import is not yet possible but should available in the
    /// next import run. The import files will not be offered to another import format in the ongoing import run.
    /// If the retention period of one of the files expires then all files in the group are discarded.
    /// </summary>
    FailedRetry,
    
    /// <summary>
    /// Import files were recognized but no group was built, e.g. because a file is corrupted. It's not expected that this state will
    /// change and so all files in the import group should be discarded at once. The import files will not be offered to another import
    /// format in the ongoing import run.
    /// </summary>
    Failed
}