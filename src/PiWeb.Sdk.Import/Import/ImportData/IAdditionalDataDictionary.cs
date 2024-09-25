#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents a collection of additional data.
/// </summary>
public interface IAdditionalDataDictionary
{
    #region properties

    /// <summary>
    /// The number of additional data contained in this entity.
    /// </summary>
    public int AdditionalDataCount { get; }

    #endregion

    #region methods

    /// <summary>
    /// Determines whether this entity contains additional data with the given name.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <returns>True if this entity contains additional data with the given name; otherwise, false.</returns>
    public bool ContainsAdditionalData(string name);

    /// <summary>
    /// Gets the additional data with the given name if it exists.
    /// </summary>
    /// <param name="name">The name of the additional data to get.</param>
    /// <param name="additionalDataItem">Receives the additional data if it exists; otherwise, null.</param>
    /// <returns>True if the additional data exists; otherwise, false.</returns>
    public bool TryGetAdditionalData(string name, [MaybeNullWhen(false)] out AdditionalDataItem additionalDataItem);

    /// <summary>
    /// Returns all contained additional data.
    /// </summary>
    /// <returns>The contained additional data.</returns>
    public IEnumerable<AdditionalDataItem> EnumerateAdditionalData();

    /// <summary>
    /// Adds the given additional data.
    /// </summary>
    /// <param name="additionalDataItem">The additional data to add.</param>
    /// <returns>The added additional data.</returns>
    /// <exception cref="ImportDataException">Thrown when additional data of the given name already exists.</exception>
    public AdditionalDataItem AddAdditionalData(AdditionalDataItem additionalDataItem);

    /// <summary>
    /// Adds new additional data.
    /// </summary>
    /// <param name="name">The name of the new additional data to add.</param>
    /// <param name="dataStream">A stream representing the content.</param>
    /// <param name="disposeStream">Specifies whether the stream should be disposed after the upload finished.</param>
    /// <returns>The new additional data item.</returns>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    /// <exception cref="ImportDataException">Thrown when the given name already exists.</exception>
    public AdditionalDataItem AddAdditionalData(string name, Stream dataStream, bool disposeStream = true);

    /// <summary>
    /// Adds the given additional data potentially replacing existing additional data of the same name.
    /// </summary>
    /// <param name="additionalDataItem">The additional data to add.</param>
    /// <param name="replacedAdditionalDataItem">
    /// Receives the replaced additional data or null when no additional data was replaced. Additional data is never
    /// replaced by itself when it already exists and null is received in such a case.
    /// </param>
    /// <returns>The added additional data.</returns>
    public AdditionalDataItem SetAdditionalData(
        AdditionalDataItem additionalDataItem,
        out AdditionalDataItem? replacedAdditionalDataItem);

    /// <summary>
    /// Adds new additional data potentially replacing existing additional data of the same name..
    /// </summary>
    /// <param name="name">The name of the new additional data item.</param>
    /// <param name="dataStream">A stream representing the content.</param>
    /// <param name="disposeStream">Specifies whether the stream should be disposed after the upload finished.</param>
    /// <param name="replacedAdditionalDataItem">
    /// Receives the replaced additional data or null when no additional data was replaced. Additional data is never
    /// replaced by itself when it already exists and null is received in such a case.
    /// </param>
    /// <returns>The new additional data item.</returns>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    public AdditionalDataItem SetAdditionalData(
        string name,
        Stream dataStream,
        bool disposeStream,
        out AdditionalDataItem? replacedAdditionalDataItem);

    /// <summary>
    /// Removes the given additional data.
    /// </summary>
    /// <param name="additionalDataItem">The additional data to remove.</param>
    /// <returns>True if the additional data was removed; otherwise, false.</returns>
    public bool RemoveAdditionalData(AdditionalDataItem additionalDataItem);

    /// <summary>
    /// Removes additional data with the given name.
    /// </summary>
    /// <param name="name">The name of the additional data to remove. This name is case insensitive.</param>
    /// <returns>True if any data was removed; otherwise, false.</returns>
    public bool RemoveAdditionalData(string name);

    /// <summary>
    /// Removes all additional data.
    /// </summary>
    public void ClearAdditionalData();

    #endregion
}