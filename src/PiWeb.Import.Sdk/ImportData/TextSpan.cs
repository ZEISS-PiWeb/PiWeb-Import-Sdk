#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportData;

/// <summary>
/// Represents position information of a slice of text. All position information is 0 based. Undefined properties are represented
/// by a negative number.
/// </summary>
public record TextSpan
{
    #region properties

    /// <summary>
    /// The absolute character position of the first character within the text. [0 - int.MaxValue]
    /// A negative value represents the undefined position.
    /// </summary>
    public int Position { get; init; } = -1;

    /// <summary>
    /// The line number of the first character within the text. [0 - int.MaxValue]
    /// A negative value represents the undefined line.
    /// </summary>
    public int Line { get; init; } = -1;

    /// <summary>
    /// The character position of the first character within the line. [0 - int.MaxValue]
    /// A negative value represents the undefined position.
    /// </summary>
    public int LinePosition { get; init; } = -1;

    /// <summary>
    /// The length of this text span in characters.
    /// A negative value represents the undefined length.
    /// </summary>
    public int Length { get; init; } = -1;

    #endregion
}