#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Common.ConfigurationItems;
using Zeiss.PiWeb.Sdk.Common.PropertyStorage;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.SecondImportAutomation;

public class AutomationConfiguration(IPropertyStorage storage) : IAutomationConfiguration
{
    // Define a new settings section
    private static readonly Section _configurationSection = new Section() { Title = "Configuration", Priority = 1 };

    [ConfigurationItem]
    public StringConfigurationItem ImportPartName { get; } = new StringConfigurationItem(storage, nameof(ImportPartName), "WeatherData")
    {
        Priority = 1,
        Section = _configurationSection,
        Title = "Import target part name",
        Tooltip = "Defines the name of the part under which all subsequent imports take place."
    };

    [ConfigurationItem]
    public StringConfigurationItem WeatherLocation { get; } = new StringConfigurationItem(storage, nameof(WeatherLocation), "Dresden")
    {
        Priority = 2,
        Section = _configurationSection,
        Title = "Weather location",
        Tooltip = "The name of the location where the weather is to be queried."
    };
}
