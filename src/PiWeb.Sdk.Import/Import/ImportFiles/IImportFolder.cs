#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Generic;

namespace Zeiss.PiWeb.Sdk.Import.ImportFiles;

/// <summary>
/// Represents a folder containing import files.
/// </summary>
public interface IImportFolder
{
    /// <summary>
    /// The path of this import folder relative to its import part folder. If this import folder is an import part
    /// folder, this property is the empty string.
    /// </summary>
    string RelativePath { get; }

    /// <summary>
    /// Returns all available import files in this import folder.
    /// </summary>
    IEnumerable<IImportFile> GetFiles();

    /// <summary>
    /// Returns the import file with the given name or null when no such file is available in this import folder.
    /// The filename is case-insensitive.
    /// </summary>
    IImportFile? FindFile(string filename);
    
    /// <summary>
    /// Return all available import files in this import folder that match the given wildcard pattern.
    /// </summary>
    /// <remarks>Supports the following wildcards: '*' and '?'. The pattern is case-insensitive.</remarks>
    /// <param name="pattern">The wildcard pattern to match.</param>
    /// <returns>The list of matching import files.</returns>
    IEnumerable<IImportFile> FindFiles(string pattern);
}