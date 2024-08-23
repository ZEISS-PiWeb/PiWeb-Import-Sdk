#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// <inheritdoc />
/// This implementation returns a stream from a given <see cref="IImportFile"/>.
/// </summary>
public sealed class ImportFileAdditionalDataItem : AdditionalDataItem
{
    #region members

    private readonly IImportFile _ImportFile;

    #endregion

    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImportFileAdditionalDataItem"/> class.
    /// </summary>
    /// <param name="importFile">The file source.</param>
    public ImportFileAdditionalDataItem(IImportFile importFile) : base(importFile.Name)
    {
        _ImportFile = importFile;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImportFileAdditionalDataItem"/> class.
    /// </summary>
    /// <param name="name">The name of the additional data.</param>
    /// <param name="importFile">The file source.</param>
    public ImportFileAdditionalDataItem(string name, IImportFile importFile) : base(name)
    {
        _ImportFile = importFile;
    }

    #endregion

    #region methods

    /// <inheritdoc />
    public override bool TryGetDataStream([MaybeNullWhen(false)] out Stream dataStream)
    {
        try
        {
            dataStream = _ImportFile.GetDataStream();
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