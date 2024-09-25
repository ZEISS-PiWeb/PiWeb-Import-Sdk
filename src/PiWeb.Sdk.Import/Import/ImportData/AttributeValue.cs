#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents a value of an entity attribute.
/// </summary>
public readonly struct AttributeValue
{
    #region members

    /// <summary>
    /// The null value.
    /// </summary>
    public static readonly AttributeValue Null = new AttributeValue();

    private readonly object? _Value;

    #endregion

    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AttributeValue"/> struct.
    /// </summary>
    public AttributeValue()
    {
        _Value = null;
    }

    private AttributeValue(object value)
    {
        _Value = value;
    }

    #endregion

    #region properties

    /// <summary>
    /// The type of the value.
    /// </summary>
    public AttributeValueType ValueType =>
        _Value switch
        {
            null => AttributeValueType.Null,
            int => AttributeValueType.Integer,
            double => AttributeValueType.Double,
            string => AttributeValueType.String,
            DateTime => AttributeValueType.DateTime,
            CatalogEntry => AttributeValueType.CatalogEntry,
            _ => AttributeValueType.Null
        };

    #endregion

    #region methods

    /// <summary>
    /// Creates a new attribute value from the given integer value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The attribute value.</returns>
    public static AttributeValue From(int value)
    {
        return new AttributeValue(value);
    }

    /// <summary>
    /// Creates a new attribute value from the given double value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The attribute value.</returns>
    public static AttributeValue From(double value)
    {
        return new AttributeValue(value);
    }

    /// <summary>
    /// Creates a new attribute value from the given string value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The attribute value.</returns>
    public static AttributeValue From(string value)
    {
        return new AttributeValue(value);
    }

    /// <summary>
    /// Creates a new attribute value from the given date and time value. The value must have a specified date time kind.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The attribute value.</returns>
    /// <exception cref="ImportDataException">Thrown when the given date time value has an unspecified date time kind.</exception>
    public static AttributeValue From(DateTime value)
    {
        if (value.Kind == DateTimeKind.Unspecified)
            throw new ImportDataException("Date time values must not have an unspecified date time kind.");

        return new AttributeValue(value);
    }

    /// <summary>
    /// Creates a new attribute value from the given catalog entry.
    /// </summary>
    /// <param name="catalogEntry">The catalog entry.</param>
    /// <returns>The attribute value.</returns>
    public static AttributeValue From(CatalogEntry catalogEntry)
    {
        return new AttributeValue(catalogEntry);
    }

    /// <summary>
    /// Gets the attribute value as object.
    /// </summary>
    /// <returns>The attribute value.</returns>
    public object? GetValue()
    {
        return _Value;
    }

    /// <summary>
    /// Checks whether the attribute value is null.
    /// </summary>
    /// <returns>True if the attribute value is null; otherwise, false.</returns>
    public bool IsNull()
    {
        return _Value is null;
    }

    /// <summary>
    /// Gets the attribute value as integer. When the attribute value is null or not an integer, null is returned.
    /// Note: This will never convert non-integer attribute values like doubles or strings to integers.
    /// </summary>
    /// <returns>The attribute value.</returns>
    public int? AsInteger()
    {
        return _Value as int?;
    }

    /// <summary>
    /// Gets the attribute value as double. When the attribute value is null or not a double, null is returned.
    /// Note: This will never convert non-double attribute values like integers or strings to doubles.
    /// </summary>
    /// <returns>The attribute value.</returns>
    public double? AsDouble()
    {
        return _Value as double?;
    }

    /// <summary>
    /// Gets the attribute value as string. When the attribute value is null or not a string, null is returned.
    /// Note: This will never convert non-string attribute values to string.
    /// </summary>
    /// <returns>The attribute value.</returns>
    public string? AsString()
    {
        if (_Value is string stringValue)
            return stringValue;

        return null;
    }

    /// <summary>
    /// Gets the attribute value as DateTime. When the attribute value is null or not a DateTime, null is returned.
    /// Note: This will never convert non-DateTime attribute values to DateTime.
    /// </summary>
    /// <returns>The attribute value.</returns>
    public DateTime? AsDateTime()
    {
        return _Value as DateTime?;
    }

    /// <summary>
    /// Gets the attribute value as catalog entry. When the attribute value is null or not a catalog entry, null is returned.
    /// Note: This will never convert non-ICatalogEntry attribute values to catalog entries.
    /// </summary>
    /// <returns>The attribute value.</returns>
    public CatalogEntry? AsCatalogEntry()
    {
        return _Value as CatalogEntry;
    }

    #endregion
}