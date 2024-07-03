#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System.Globalization;
using Zeiss.PiWeb.Import.Sdk.Logging;

#endregion

namespace Zeiss.PiWeb.Import.Sdk.LocalizationHandler;

/// <summary>
/// Represents the context of a localization call.
/// </summary>
public interface ILocalizationContext
{
    /// <summary>
    /// Represents the target language of the translation.
    /// </summary>
    CultureInfo TranslationCulture { get; }

    /// <summary>
    /// Represents the format to use when formatting arguments.
    /// </summary>
    CultureInfo FormatCulture { get; }

    /// <summary>
    /// The version of the plugin that created the structured text to localize. For previously persisted
    /// structured text (like automation activities or automation events), this will be the version of the plugin
    /// that created the entries originally. 
    /// </summary>
    string OriginPluginVersion { get; }

    /// <summary>
    /// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the
    /// hosting application.
    /// </summary>
    ILogger Logger { get; }
}