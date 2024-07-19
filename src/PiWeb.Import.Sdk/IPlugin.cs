#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk;

using System;
using System.Threading.Tasks;
using Zeiss.PiWeb.Import.Sdk.LocalizationHandler;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportFormat;

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
    Task Init(IPluginContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Creates a new instance of an <see cref="IImportAutomation"/>
    /// or <c>null</c> if this plugin does not provide an import automation.
    /// </summary>
    /// <param name="context">Contains information about the environment.</param>
    /// <exception cref="ModuleNotImplementedException">Thrown when module is not implemented.</exception>
    IImportAutomation GetImportAutomation(IImportAutomationContext context)
    {
        throw new ModuleNotImplementedException("No import automation module implemented");
    }

    /// <summary>
    /// Creates a new instance of an <see cref="IImportFormat"/>
    /// or <c>null</c> if this plugin does not provide an import format.
    /// </summary>
    /// <param name="context">Contains information about the environment.</param>
    /// <exception cref="ModuleNotImplementedException">Thrown when module is not implemented.</exception>
    IImportFormat GetImportFormat(IImportFormatContext context)
    {
        throw new ModuleNotImplementedException("No import format module implemented");
    }

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