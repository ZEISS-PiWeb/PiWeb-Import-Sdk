#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Import.Sdk.LocalizationHandler;

/// <summary>
/// Responsible for localizing and formatting messages and other text. 
/// </summary>
public interface ILocalizationHandler
{
    /// <summary>
    /// Localizes and formats the given format string and its parameters.
    /// </summary>
    /// <param name="text">The format string to localize.</param>
    /// <param name="args">
    /// The arguments of the format string to localize. Note that any arguments given for formatting which are not of
    /// type int, double or DateTimeOffset are always transformed implicitely to string beforehand. 
    /// </param>
    /// <param name="context">
    /// The context of the localization.
    /// This context contains information about the translation target language.
    /// </param>
    /// <returns>
    /// The localized and formatted text or null when no localization for the given parameters exists.
    /// </returns>
    /// <exception cref="FormatException">Thrown when formatting fails.</exception>
    string? LocalizeAndFormatText(string text, object[] args, ILocalizationContext context);
}