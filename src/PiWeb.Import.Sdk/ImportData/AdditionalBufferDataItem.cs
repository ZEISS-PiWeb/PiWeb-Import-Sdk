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
/// <inheritdoc />
/// This implementation always returns a memory stream backed by a given buffer.
/// </summary>
public sealed class AdditionalBufferDataItem : AdditionalDataItem
{
    #region members

    private readonly byte[] _Buffer;

    #endregion

    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AdditionalStreamDataItem"/> class.
    /// </summary>
    /// <param name="name">The name of the additional data.</param>
    /// <param name="buffer">The buffer containing the additional data.</param>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    public AdditionalBufferDataItem(string name, byte[] buffer) : base(name)
    {
        _Buffer = buffer;
    }

    #endregion

    #region methods

    /// <inheritdoc />
    public override bool TryGetDataStream([MaybeNullWhen(false)] out Stream dataStream)
    {
        dataStream = new MemoryStream(_Buffer, 0, _Buffer.Length, false, true);
        return true;
    }

    /// <inheritdoc />
    public override void DisposeStream(Stream dataStream)
    {
        dataStream.Dispose();
    }

    #endregion
}