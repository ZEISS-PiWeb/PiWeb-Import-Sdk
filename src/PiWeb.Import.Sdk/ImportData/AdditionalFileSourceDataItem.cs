#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

#region usings

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Modules.ImportFormat;

#endregion

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
        try
        {
            dataStream = _FileSource.GetDataStream();
            return true;
        }
        catch (Exception)
        {
            dataStream = null;
            return false;
        }
    }

    /// <inheritdoc />
    public override void DisposeStream(Stream dataStream)
    {
        dataStream.Dispose();
    }

    #endregion
}