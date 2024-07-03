#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

#region usings

using System;
using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.LocalizationHandler;

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
    /// <param name="context">Contains information about the environment.</param>
    /// <exception cref="ModuleRegistrationException">Thrown when module registration fails.</exception>
    Task Init(IPluginContext context);

    /// <summary>
    /// This method is called once to get a localization handler for the plugin. The handler is used internally
    /// to localize messages and other text that is persisted. Methods that use this localization handler internally
    /// are documented as such. When this method is not implemented, a default localization handler will be used.
    /// Usually the default handler will not translate at all and use
    /// <see cref="string.Format(IFormatProvider?, string, object[])"/>
    /// to format. 
    /// </summary>
    /// <param name="context">
    /// Contains additional information for creating the <see cref="ILocalizationHandler"/> instance.
    /// </param> 
    /// <returns>The custom localization handler.</returns>
    ILocalizationHandler GetLocalizationHandler(ILocalizationHandlerContext context)
    {
        return new FormatOnlyLocalizationHandler();
    }

    #endregion
}