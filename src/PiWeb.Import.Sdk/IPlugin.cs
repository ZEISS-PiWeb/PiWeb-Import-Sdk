#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.LocalizationHandler;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

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
    /// <param name="context">Contains information about the environment this plugin is hosted in.</param>
    Task InitAsync(IPluginInitContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Creates a new instance of an <see cref="IImportAutomation"/>.
    /// </summary>
    /// <param name="context">Contains information about the environment.</param>
    /// <exception cref="ModuleNotImplementedException">
    /// Thrown when the plugin does not implement an import automation.
    /// </exception>
    IImportAutomation CreateImportAutomation(ICreateImportAutomationContext context)
    {
        throw new ModuleNotImplementedException("No import automation module implemented.");
    }

    /// <summary>
    /// Creates a new instance of an <see cref="IImportFormat"/>.
    /// </summary>
    /// <param name="context">Contains information about the environment.</param>
    /// <exception cref="ModuleNotImplementedException">
    /// Thrown when the plugin does not implement an import format.
    /// </exception>
    IImportFormat CreateImportFormat(ICreateImportFormatContext context)
    {
        throw new ModuleNotImplementedException("No import format module implemented.");
    }

    /// <summary>
    /// Creates a new instance of <see cref="ILocalizationHandler"/>. This method is called once after the plugin is
    /// loaded to get a localization handler for the plugin. The handler is used internally to localize messages and
    /// other text that is persisted. SDK methods that use this localization handler internally are documented as such.
    /// When this method is not implemented, a default localization handler will be used. Usually the default handler
    /// will not translate at all and use <see cref="string.Format(IFormatProvider?, string, object[])"/> to format. 
    /// </summary>
    /// <param name="context">
    /// Contains additional information for creating the <see cref="ILocalizationHandler"/> instance.
    /// </param> 
    /// <returns>The custom localization handler.</returns>
    ILocalizationHandler CreateLocalizationHandler(ICreateLocalizationHandlerContext context)
    {
        return new FormatOnlyLocalizationHandler();
    }

    #endregion
}