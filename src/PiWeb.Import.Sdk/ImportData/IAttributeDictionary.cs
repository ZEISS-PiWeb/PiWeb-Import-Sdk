#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents a collection of attributes.
/// </summary>
public interface IAttributeDictionary
{
    #region properties

    /// <summary>
    /// The number of contained attributes.
    /// </summary>
    public int AttributeCount { get; }

    #endregion

    #region methods

    /// <summary>
    /// Determines whether this entity contains an attribute for the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to look for.</param>
    /// <returns>True if this entity contains an attribute for the given attribute key; otherwise, false.</returns>
    public bool ContainsAttribute(ushort attributeKey);

    /// <summary>
    /// Gets the attribute value for the given attribute key if it exists.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute value to get.</param>
    /// <param name="attributeValue">Receives the attribute value if it exists; otherwise, the null attribute value.</param>
    /// <returns>True if the attribute value exists; otherwise, false.</returns>
    public bool TryGetAttributeValue(ushort attributeKey, out AttributeValue attributeValue);

    /// <summary>
    /// Returns all contained attributes.
    /// </summary>
    /// <returns>The contained attributes.</returns>
    public IEnumerable<KeyValuePair<ushort, AttributeValue>> EnumerateAttributes();

    /// <summary>
    /// Removes the attribute value of the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The attribute key identifying the value to remove.</param>
    /// <returns>True if the attribute was removed; otherwise, false.</returns>
    public bool RemoveAttribute(ushort attributeKey);

    /// <summary>
    /// Sets the given attribute value for the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to set the value for.</param>
    /// <param name="attributeValue">The attribute value to set.</param>
    public void SetAttribute(ushort attributeKey, AttributeValue attributeValue);

    /// <summary>
    /// Sets the given attribute value for the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to set the value for.</param>
    /// <param name="intValue">The attribute value to set.</param>
    public void SetAttribute(ushort attributeKey, int intValue);

    /// <summary>
    /// Sets the given attribute value for the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to set the value for.</param>
    /// <param name="doubleValue">The attribute value to set.</param>
    public void SetAttribute(ushort attributeKey, double doubleValue);

    /// <summary>
    /// Sets the given attribute value for the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to set the value for.</param>
    /// <param name="stringValue">The attribute value to set.</param>
    public void SetAttribute(ushort attributeKey, string stringValue);

    /// <summary>
    /// Sets the given attribute value for the given attribute key.
    /// </summary>
    /// <param name="attributeKey">The key of the attribute to set the value for.</param>
    /// <param name="dateTimeValue">The attribute value to set.</param>
    public void SetAttribute(ushort attributeKey, DateTime dateTimeValue);

    #endregion
}