#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Zeiss.PiWeb.Import.Sdk.Collections;

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

internal struct EmbeddedAdditionalDataCollection
{
    #region members

    private ResizableArray<AdditionalDataItem> _AdditionalDataList = new ResizableArray<AdditionalDataItem>(0);

    #endregion

    #region constructors

    public EmbeddedAdditionalDataCollection()
    {
    }

    #endregion

    #region properties

    public int Count => _AdditionalDataList.Count;

    #endregion

    #region methods

    public bool Contains(string name)
    {
        for (var i = 0; i < _AdditionalDataList.Count; ++i)
        {
            if (string.Equals(_AdditionalDataList[i].Name, name, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    public bool TryGetAdditionalData(string name, [MaybeNullWhen(false)] out AdditionalDataItem additionalDataItem)
    {
        for (var i = 0; i < _AdditionalDataList.Count; ++i)
        {
            if (!string.Equals(_AdditionalDataList[i].Name, name, StringComparison.OrdinalIgnoreCase))
                continue;

            additionalDataItem = _AdditionalDataList[i];
            return true;
        }

        additionalDataItem = null;
        return false;
    }

    public IEnumerable<AdditionalDataItem> GetAdditionalData()
    {
        return _AdditionalDataList.Items.Take(_AdditionalDataList.Count);
    }

    public bool TryAdd(AdditionalDataItem additionalDataItem)
    {
        for (var i = 0; i < _AdditionalDataList.Count; ++i)
        {
            if (string.Equals(additionalDataItem.Name, _AdditionalDataList[i].Name, StringComparison.OrdinalIgnoreCase))
                return false;
        }

        _AdditionalDataList.Add(additionalDataItem);
        return true;
    }

    public void Set(AdditionalDataItem additionalDataItem, out AdditionalDataItem? replacedAdditionalDataItem)
    {
        for (var i = 0; i < _AdditionalDataList.Count; ++i)
        {
            if (string.Equals(additionalDataItem.Name, _AdditionalDataList[i].Name, StringComparison.OrdinalIgnoreCase))
            {
                replacedAdditionalDataItem = _AdditionalDataList[i];
                _AdditionalDataList[i] = additionalDataItem;
                return;
            }
        }

        replacedAdditionalDataItem = null;
        _AdditionalDataList.Add(additionalDataItem);
    }

    public bool Remove(AdditionalDataItem additionalDataItem)
    {
        for (var i = 0; i < _AdditionalDataList.Count; ++i)
        {
            if (!ReferenceEquals(additionalDataItem, _AdditionalDataList[i]))
                continue;

            _AdditionalDataList.RemoveAt(i);
            return true;
        }

        return false;
    }

    public bool Remove(string name)
    {
        for (var i = 0; i < _AdditionalDataList.Count; ++i)
        {
            if (!string.Equals(_AdditionalDataList[i].Name, name, StringComparison.OrdinalIgnoreCase))
                continue;

            _AdditionalDataList.RemoveAt(i);
            return true;
        }

        return false;
    }

    public void Clear()
    {
        _AdditionalDataList.Clear();
    }

    #endregion
}