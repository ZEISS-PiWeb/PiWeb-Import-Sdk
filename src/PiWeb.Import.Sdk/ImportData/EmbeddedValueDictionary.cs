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

internal struct EmbeddedValueDictionary<TKey, TValue, TComp>
    where TComp : IStaticEqualityComparer<TKey>
{
    #region members

    private ResizableArray<KeyValuePair<TKey, TValue>> _Entries =
        new ResizableArray<KeyValuePair<TKey, TValue>>(0);

    #endregion

    #region constructors

    public EmbeddedValueDictionary()
    {
    }

    #endregion

    #region properties

    /// <summary>
    /// The number of contained attributes.
    /// </summary>
    public int Count => _Entries.Count;

    #endregion

    #region methods

    /// <summary>
    /// Determines whether a value for the given key is present.
    /// </summary>
    /// <param name="attributeKey">The key of the value to look for.</param>
    /// <returns>True if a value for the given key is present; otherwise, false.</returns>
    public bool ContainsKey(TKey attributeKey)
    {
        for (var i = 0; i < _Entries.Count; ++i)
        {
            if (TComp.Equals(_Entries[i].Key, attributeKey))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the value for the given key if it exists.
    /// </summary>
    /// <param name="attributeKey">The key of the value to get.</param>
    /// <param name="attributeValue">Receives the value if it exists; otherwise, null.</param>
    /// <returns>True if the value exists; otherwise, false.</returns>
    public bool TryGetValue(TKey attributeKey, [MaybeNullWhen(false)] out TValue attributeValue)
    {
        for (var i = 0; i < _Entries.Count; ++i)
        {
            if (!TComp.Equals(_Entries[i].Key, attributeKey))
                continue;

            attributeValue = _Entries[i].Value;
            return true;
        }

        attributeValue = default;
        return false;
    }

    /// <summary>
    /// Returns all contained values as key value pairs.
    /// </summary>
    /// <returns>The contained values as key value pairs.</returns>
    public IEnumerable<KeyValuePair<TKey, TValue>> GetKeyValues()
    {
        return _Entries.Items.Take(_Entries.Count);
    }

    /// <summary>
    /// Sets the given value for the given key.
    /// </summary>
    /// <param name="attributeKey">The key to set the value for.</param>
    /// <param name="attributeValue">The value to set.</param>
    public void SetValue(TKey attributeKey, TValue attributeValue)
    {
        for (var i = 0; i < _Entries.Count; ++i)
        {
            if (!TComp.Equals(_Entries[i].Key, attributeKey))
                continue;

            _Entries[i] = new KeyValuePair<TKey, TValue>(attributeKey, attributeValue);
            return;
        }

        _Entries.Add(new KeyValuePair<TKey, TValue>(attributeKey, attributeValue));
    }

    /// <summary>
    /// Removes the value of the given key.
    /// </summary>
    /// <param name="attributeKey">The key identifying the value to remove.</param>
    /// <returns>True if the value was removed; otherwise, false.</returns>
    public bool Remove(TKey attributeKey)
    {
        for (var i = 0; i < _Entries.Count; ++i)
        {
            if (!TComp.Equals(_Entries[i].Key, attributeKey))
                continue;

            _Entries.RemoveAt(i);
            return true;
        }

        return false;
    }

    #endregion
}

internal interface IStaticEqualityComparer<in T>
{
    #region methods

    public static abstract bool Equals(T lhs, T rhs);

    #endregion
}

internal sealed class StaticUShortEqualityComparer : IStaticEqualityComparer<ushort>
{
    #region constructors

    private StaticUShortEqualityComparer()
    {
    }

    #endregion

    #region interface IStaticEqualityComparer<ushort>

    public static bool Equals(ushort lhs, ushort rhs)
    {
        return lhs == rhs;
    }

    #endregion
}

internal sealed class StaticOrdinalStringEqualityComparer : IStaticEqualityComparer<string>
{
    #region constructors

    private StaticOrdinalStringEqualityComparer()
    {
    }

    #endregion

    #region interface IStaticEqualityComparer<string>

    public static bool Equals(string lhs, string rhs)
    {
        return StringComparer.Ordinal.Equals(lhs, rhs);
    }

    #endregion
}