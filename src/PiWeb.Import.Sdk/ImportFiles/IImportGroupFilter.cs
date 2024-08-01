#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.ImportFiles.Exceptions;

namespace Zeiss.PiWeb.Import.Sdk.ImportFiles;

/// <summary>
/// Responsible for filtering import groups. Each import format is represented by one import group filter.
/// When grouping new import files, a chain of import group filters is built. This chain consists of one import filter
/// per import format ordered by the priority of the import format.
/// An initial import group (consisting only of one import file) is then presented in order to each filter in the chain
/// until a filter returns a filter result other than <see cref="FilterResult.None"/>. This determines which format is
/// responsible for handling this import group and how it needs to be handled. See <see cref="FilterResult"/> for all
/// possible results.
/// When presented an import group, import group filters may also add additional import files to this group. 
/// </summary>
public interface IImportGroupFilter
{
    /// <summary>
    /// Filters the given import group and adds potential dependencies to complete it. A filter result of
    /// <see cref="FilterResult.None"/> indicates that the given group should not be filtered out. It will then be
    /// passed on to the next import group filter in the chain.
    /// </summary>
    /// <param name="importGroup">The import group to filter.</param>
    /// <param name="context">
    /// The context of the filter operation. It contains information about other import files that are available
    /// to potentially satisfy any dependencies.
    /// </param>
    /// <returns>The filter result.</returns>
    /// <exception cref="ImportFileException">
    /// Thrown when opening any import file during the filter operation failed. The result is similar to returning
    /// <see cref="FilterResult.RetryOrDiscard"/> but an error message is automatically added to the import history.  
    /// </exception>
    ValueTask<FilterResult> FilterAsync(IImportGroup importGroup, IFilterContext context);
}