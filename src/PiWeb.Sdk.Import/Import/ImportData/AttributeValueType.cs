#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportData;

/// <summary>
/// Represents the possible value types of attributes.
/// </summary>
public enum AttributeValueType
{
    /// <summary>
    /// The null attribute value.
    /// </summary>
    Null,

    /// <summary>
    /// An integer attribute value.
    /// </summary>
    Integer,

    /// <summary>
    /// A floating point attribute value.
    /// </summary>
    Double,

    /// <summary>
    /// A string attribute value.
    /// </summary>
    String,

    /// <summary>
    /// A date and time attribute value.
    /// </summary>
    DateTime,

    /// <summary>
    /// A catalog entry attribute value.
    /// </summary>
    CatalogEntry
}