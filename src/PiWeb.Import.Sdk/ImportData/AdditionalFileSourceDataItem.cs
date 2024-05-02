#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// <inheritdoc />
/// This implementation returns a stream from a given <see cref="IFileSource"/>.
/// </summary>
public sealed class AdditionalFileSourceDataItem : AdditionalDataItem
{
    #region members

    private readonly IFileSource _FileSource;

    #endregion

    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AdditionalFileSourceDataItem"/> class.
    /// </summary>
    /// <param name="name">The name of the additional data.</param>
    /// <param name="fileSource">The file source.</param>
    public AdditionalFileSourceDataItem(string name, IFileSource fileSource) : base(name)
    {
        _FileSource = fileSource;
    }

    #endregion

    #region methods

    /// <inheritdoc />
    public override bool TryGetDataStream([MaybeNullWhen(false)] out Stream dataStream)
    {
        return _FileSource.TryGetDataStream(out dataStream);
    }

    /// <inheritdoc />
    public override void DisposeStream(Stream dataStream)
    {
        dataStream.Dispose();
    }

    #endregion
}