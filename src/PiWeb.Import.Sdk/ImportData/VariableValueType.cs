#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents the possible value types of variables.
/// </summary>
public enum VariableValueType
{
    /// <summary>
    /// The null variable value.
    /// </summary>
    Null,

    /// <summary>
    /// An integer variable value.
    /// </summary>
    Integer,

    /// <summary>
    /// A floating point variable value.
    /// </summary>
    Double,

    /// <summary>
    /// A string variable value.
    /// </summary>
    String,

    /// <summary>
    /// A date and time variable value.
    /// </summary>
    DateTime,

    /// <summary>
    /// A catalog entry variable value.
    /// </summary>
    CatalogEntry
}