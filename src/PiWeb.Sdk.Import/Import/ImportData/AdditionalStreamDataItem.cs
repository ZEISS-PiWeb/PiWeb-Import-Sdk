#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// <inheritdoc />
/// This implementation always returns the same given stream when <see cref="TryGetDataStream"/> is called. When <see cref="DisposeStream"/>
/// is called the given stream may or may not be disposed. If it is disposed, <see cref="TryGetDataStream"/> will never succeed afterward.
/// </summary>
public sealed class AdditionalStreamDataItem : AdditionalDataItem
{
    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AdditionalStreamDataItem"/> class.
    /// </summary>
    /// <param name="name">The name of the additional data.</param>
    /// <param name="dataStream">A stream representing the content.</param>
    /// <param name="disposeStream">Specifies whether the given stream should be disposed when <see cref="DisposeStream"/> is called.</param>
    /// <exception cref="ImportDataException">Thrown when the given name is empty.</exception>
    public AdditionalStreamDataItem(string name, Stream dataStream, bool disposeStream = true) : base(name)
    {
        _DataStream = dataStream;
        _DisposeStream = disposeStream;
    }

    #endregion

    /// <inheritdoc />
    public override bool TryGetDataStream([MaybeNullWhen(false)] out Stream dataStream)
    {
        if (_StreamIsDisposed)
        {
            dataStream = null;
            return false;
        }

        dataStream = _DataStream;
        return true;
    }

    /// <inheritdoc />
    public override void DisposeStream(Stream dataStream)
    {
        if (!_DisposeStream || !ReferenceEquals(dataStream, _DataStream))
            return;

        dataStream.Dispose();
        _StreamIsDisposed = true;
    }

    #region members

    private readonly Stream _DataStream;
    private readonly bool _DisposeStream;
    private bool _StreamIsDisposed;

    #endregion
}