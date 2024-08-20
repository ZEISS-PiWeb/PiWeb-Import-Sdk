#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents an item of additional data.
/// </summary>
public abstract class AdditionalDataItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdditionalDataItem"/> class.
    /// </summary>
    /// <param name="name">The name of the additional data.</param>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    protected AdditionalDataItem(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ImportDataException("The name of additional data must be non-empty.");

        Name = name;
    }

    #region properties

    /// <summary>
    /// The name of this additional data item.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The mime type of the additional data item. This value is null when no mime type is specified.
    /// </summary>
    public string MimeType { get; set; } = string.Empty;

    #endregion

    #region methods

    /// <summary>
    /// Gets a <see cref="Stream"/> of the contents of this additional data.
    /// </summary>
    public abstract bool TryGetDataStream([MaybeNullWhen(false)] out Stream dataStream);

    /// <summary>
    /// Disposes of a stream received by <see cref="TryGetDataStream"/> after the upload is finished.
    /// </summary>
    /// <param name="dataStream">The stream to dispose.</param>
    public virtual void DisposeStream(Stream dataStream)
    {
        // Since we do not know how this stream was created, we do nothing here unless some other behavior is
        // implemented by overriding this method.
    }

    #endregion
}