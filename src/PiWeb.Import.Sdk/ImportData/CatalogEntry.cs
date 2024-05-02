#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents a catalog entry that can be used as an attribute value.
/// </summary>
public sealed class CatalogEntry : IAttributeDictionary
{
    #region members

    private EmbeddedValueDictionary<ushort, AttributeValue, StaticUShortEqualityComparer> _Attributes =
        new EmbeddedValueDictionary<ushort, AttributeValue, StaticUShortEqualityComparer>();

    #endregion

    #region methods

    private static void ValidateAttributeValue(AttributeValue attributeValue)
    {
        if (attributeValue.ValueType is AttributeValueType.CatalogEntry)
            throw new ImportDataException("Catalog entries cannot be used as attributes in other catalog entries.");
    }

    #endregion

    #region interface IAttributeDictionary

    /// <inheritdoc />
    public int AttributeCount => _Attributes.Count;

    /// <inheritdoc />
    public bool ContainsAttribute(ushort attributeKey)
    {
        return _Attributes.ContainsKey(attributeKey);
    }

    /// <inheritdoc />
    public bool TryGetAttributeValue(ushort attributeKey, out AttributeValue attributeValue)
    {
        return _Attributes.TryGetValue(attributeKey, out attributeValue);
    }

    /// <inheritdoc />
    public IEnumerable<KeyValuePair<ushort, AttributeValue>> EnumerateAttributes()
    {
        return _Attributes.GetKeyValues();
    }

    /// <inheritdoc />
    public void SetAttribute(ushort attributeKey, AttributeValue attributeValue)
    {
        ValidateAttributeValue(attributeValue);
        _Attributes.SetValue(attributeKey, attributeValue);
    }

    /// <inheritdoc />
    public void SetAttribute(ushort attributeKey, int intValue)
    {
        SetAttribute(attributeKey, AttributeValue.From(intValue));
    }

    /// <inheritdoc />
    public void SetAttribute(ushort attributeKey, double doubleValue)
    {
        SetAttribute(attributeKey, AttributeValue.From(doubleValue));
    }

    /// <inheritdoc />
    public void SetAttribute(ushort attributeKey, string stringValue)
    {
        SetAttribute(attributeKey, AttributeValue.From(stringValue));
    }

    /// <inheritdoc />
    public void SetAttribute(ushort attributeKey, DateTime dateTimeValue)
    {
        SetAttribute(attributeKey, AttributeValue.From(dateTimeValue));
    }

    /// <inheritdoc />
    public bool RemoveAttribute(ushort attributeKey)
    {
        return _Attributes.Remove(attributeKey);
    }

    #endregion
}