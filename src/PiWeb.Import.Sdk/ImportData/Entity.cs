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
using System.Diagnostics.CodeAnalysis;
using System.IO;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents an entity to be imported.
/// </summary>
public abstract class Entity(Guid uuid) : IExtendedAttributeDictionary, IVariableDictionary, IAdditionalDataDictionary
{
    #region members

    private EmbeddedValueDictionary<ushort, AttributeValue, StaticUShortEqualityComparer> _Attributes =
        new EmbeddedValueDictionary<ushort, AttributeValue, StaticUShortEqualityComparer>();

    private EmbeddedValueDictionary<string, VariableValue, StaticOrdinalStringEqualityComparer> _Variables =
        new EmbeddedValueDictionary<string, VariableValue, StaticOrdinalStringEqualityComparer>();

    private EmbeddedAdditionalDataCollection _AdditionalData = new EmbeddedAdditionalDataCollection();

    #endregion

    #region properties

    /// <summary>
    /// The type of this entity.
    /// </summary>
    public abstract EntityType EntityType { get; }

    /// <summary>
    /// A unique id identifying this entity.
    /// </summary>
    public Guid Uuid { get; } = uuid;

    #endregion

    #region methods

    /// <summary>
    /// Creates a new empty catalog entry and sets it as the value of the attribute given by its attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to set.</param>
    /// <returns>The new empty catalog entry.</returns>
    public CatalogEntry SetCatalogEntryAttribute(ushort attributeKey)
    {
        var newEntry = new CatalogEntry();
        SetAttribute(attributeKey, AttributeValue.From(newEntry));
        return newEntry;
    }

    /// <summary>
    /// Creates a new empty catalog entry and sets it as the value of the variable given by its variable name.
    /// </summary>
    /// <param name="variableName">The name of the variable to set.</param>
    /// <returns>The new empty catalog entry.</returns>
    public CatalogEntry SetCatalogEntryVariable(string variableName)
    {
        var newEntry = new CatalogEntry();
        SetVariable(variableName, VariableValue.From(newEntry));
        return newEntry;
    }

    #endregion

    #region interface IAdditionalDataDictionary

    /// <inheritdoc />
    public int AdditionalDataCount => _AdditionalData.Count;

    /// <inheritdoc />
    public bool ContainsAdditionalData(string name)
    {
        return _AdditionalData.Contains(name);
    }

    /// <inheritdoc />
    public bool TryGetAdditionalData(string name, [MaybeNullWhen(false)] out AdditionalDataItem additionalDataItem)
    {
        return _AdditionalData.TryGetAdditionalData(name, out additionalDataItem);
    }

    /// <inheritdoc />
    public IEnumerable<AdditionalDataItem> EnumerateAdditionalData()
    {
        return _AdditionalData.GetAdditionalData();
    }

    /// <inheritdoc />
    public AdditionalDataItem AddAdditionalData(AdditionalDataItem additionalDataItem)
    {
        if (!_AdditionalData.TryAdd(additionalDataItem))
        {
            throw new ImportDataException(
                "Cannot add additional data because additional data with this name already exists.");
        }

        return additionalDataItem;
    }

    /// <inheritdoc />
    public AdditionalDataItem AddAdditionalData(string name, Stream dataStream, bool disposeStream = true)
    {
        return AddAdditionalData(new AdditionalStreamDataItem(name, dataStream, disposeStream));
    }

    /// <inheritdoc />
    public AdditionalDataItem SetAdditionalData(
        AdditionalDataItem additionalDataItem,
        out AdditionalDataItem? replacedAdditionalDataItem)
    {
        _AdditionalData.Set(additionalDataItem, out replacedAdditionalDataItem);
        return additionalDataItem;
    }

    /// <inheritdoc />
    public AdditionalDataItem SetAdditionalData(
        string name,
        Stream dataStream,
        bool disposeStream,
        out AdditionalDataItem? replacedAdditionalDataItem)
    {
        return SetAdditionalData(
            new AdditionalStreamDataItem(name, dataStream, disposeStream),
            out replacedAdditionalDataItem);
    }

    /// <inheritdoc />
    public bool RemoveAdditionalData(AdditionalDataItem additionalDataItem)
    {
        return _AdditionalData.Remove(additionalDataItem);
    }

    /// <inheritdoc />
    public bool RemoveAdditionalData(string name)
    {
        return _AdditionalData.Remove(name);
    }

    /// <inheritdoc />
    public void ClearAdditionalData()
    {
        _AdditionalData.Clear();
    }

    #endregion

    #region interface IExtendedAttributeDictionary

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
    public void SetAttribute(ushort attributeKey, CatalogEntry catalogEntry)
    {
        SetAttribute(attributeKey, AttributeValue.From(catalogEntry));
    }

    /// <inheritdoc />
    public bool RemoveAttribute(ushort attributeKey)
    {
        return _Attributes.Remove(attributeKey);
    }

    #endregion

    #region interface IVariableDictionary

    /// <inheritdoc />
    public int VariableCount => _Variables.Count;

    /// <inheritdoc />
    public bool ContainsVariable(string variableName)
    {
        return _Variables.ContainsKey(variableName);
    }

    /// <inheritdoc />
    public bool TryGetVariableValue(string variableName, out VariableValue variableValue)
    {
        return _Variables.TryGetValue(variableName, out variableValue);
    }

    /// <inheritdoc />
    public IEnumerable<KeyValuePair<string, VariableValue>> EnumerateVariables()
    {
        return _Variables.GetKeyValues();
    }

    /// <inheritdoc />
    public void SetVariable(string variableName, VariableValue variableValue)
    {
        _Variables.SetValue(variableName, variableValue);
    }

    /// <inheritdoc />
    public void SetVariable(string variableName, int intValue)
    {
        _Variables.SetValue(variableName, VariableValue.From(intValue));
    }

    /// <inheritdoc />
    public void SetVariable(string variableName, double doubleValue)
    {
        _Variables.SetValue(variableName, VariableValue.From(doubleValue));
    }

    /// <inheritdoc />
    public void SetVariable(string variableName, string stringValue)
    {
        _Variables.SetValue(variableName, VariableValue.From(stringValue));
    }

    /// <inheritdoc />
    public void SetVariable(string variableName, DateTime dateTimeValue)
    {
        _Variables.SetValue(variableName, VariableValue.From(dateTimeValue));
    }

    /// <inheritdoc />
    public void SetVariable(string variableName, CatalogEntry catalogEntry)
    {
        _Variables.SetValue(variableName, VariableValue.From(catalogEntry));
    }

    /// <inheritdoc />
    public bool RemoveVariable(string variableName)
    {
        return _Variables.Remove(variableName);
    }

    #endregion
}