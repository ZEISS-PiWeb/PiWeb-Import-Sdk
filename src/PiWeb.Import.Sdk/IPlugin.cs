#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System.Globalization;
using System.Threading.Tasks;

#endregion

namespace Zeiss.PiWeb.Import.Sdk;

/// <summary>
/// Represents a PiWeb Auto Importer plugin.
/// </summary>
public interface IPlugin
{
    #region methods

    /// <summary>
    /// Initializes the plugin. Usually called during startup of the hosting application while showing a splash screen.
    /// Startup finishes when the returned task is completed.
    /// </summary>
    /// <param name="context">Contains information about the hosting application.</param>
    /// <exception cref="ModuleRegistrationException">Thrown when module registration fails.</exception>
    Task Init(IPluginContext context);

    /// <summary>
    /// This method can be implemented to define a custom localization for persisted text. This allows persisted text
    /// to be displayed in the current culture even if it was persisted in the context of another culture.
    /// When this method returns <c>null</c>, the original text will be used without translation. This is also the
    /// default implementation.
    /// </summary>
    /// <param name="localizationCulture">Specifies the target language.</param>
    /// <param name="text">The text or format string to translate.</param>
    /// <returns>The localized text or <c>null</c> when the original text should be used.</returns>
    string? LocalizePersistedText(CultureInfo localizationCulture, string text)
    {
        return null;
    }

    /// <summary>
    /// This method can be implemented to define a custom formatting of format strings for persisted text. This method
    /// is called after translation by <see cref="LocalizePersistedText"/> and can be used to format text after
    /// translation.
    /// When this method returns <c>null</c>, the built-in formatting is used which is equivalent to
    /// <see cref="string.Format(System.IFormatProvider?,string,object?)"/>.
    /// </summary>
    /// <param name="formatCulture">The culture to use when formatting arguments.</param>
    /// <param name="text">The localized format string.</param>
    /// <param name="args">The arguments.</param>
    /// <returns>The formatted text or <c>null</c> when the built-in formatting should be queried.</returns>
    string? FormatPersistedText(CultureInfo formatCulture, string text, params object[] args)
    {
        return null;
    }

    #endregion
}