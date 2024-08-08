---
layout: default
nav_order: 4
parent: Advanced topics
title: Configuration & storage rewrite
---

# {{ page.title }}

<!---
Ziele:
- erklären, wann und wieso ein configuration bzw. storage rewrite notwendig ist

Inhalt:
- Anwendungsfälle:
    - Veränderung der Konfiguration beim Laden des Plug-ins
    - Veränderung der Konfiguration bei Prüfplanduplikation
- Motivation hinter rewrite:
    - verhindern, dass man sich mit alten Storage Layouts beschäftigen muss
--->

## IAutomationConfiguration
As already known, the desired configurations items are defined in the IAutomationConfiguration implementation:
```c#
using Zeiss.PiWeb.Import.Sdk.ConfigurationItems;
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

public class ImportConfiguration(IAutomationConfigurationContext context) : IAutomationConfiguration
{
    [ConfigurationItem]
    public StringConfigurationItem MigrationValue { get; } = new(context.PropertyStorage, "Migration", "Original Value")
    {
        Section = WellKnownSections.Source,
        Priority = WellKnownPriorities.Source.ImportSource + 1,
        Title = "Migration"
    };
    
    [ConfigurationItem]
    public StringConfigurationItem UniqueValue { get; } = new(context.PropertyStorage, "Unique", Guid.NewGuid().ToString())
    {
        Section = WellKnownSections.Source,
        Priority = WellKnownPriorities.Source.ImportSource + 2,
        Title = "Unique"
    };
}
```

## RewritePropertyStorage in IImportAutomation
IImportAutomation provides the **RewritePropertyStorage** method. Within this method, the **RewriteReason** can be queried in the context and the user's own configuration can be set accordingly. For example, an ID can be regenerated:
```c#
using Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;
using Zeiss.PiWeb.Import.Sdk.PropertyStorage;

public class ImportAutomation : IImportAutomation
{
    public Task<IImportRunner> CreateImportRunnerAsync(IImportRunnerContext context)
    {
        return Task.FromResult<IImportRunner>(new ImportRunner(context));
    }

    public IAutomationConfiguration CreateConfiguration(IAutomationConfigurationContext context)
    {
        return new ImportConfiguration(context);
    }

    public void RewritePropertyStorage(IPropertyStorage propertyStorage, IRewriteContext context)
    {
        if (context.RewriteReason == RewriteReason.Migration)
            propertyStorage.WriteString("Migration", "Rewritten");

        if (context.RewriteReason == RewriteReason.Duplication)
            propertyStorage.WriteString("Unique", Guid.NewGuid().ToString());
    }
}
```

### RewriteReason
```c#
namespace Zeiss.PiWeb.Import.Sdk.PropertyStorage
{
  /// <summary>
  /// Enumerates possible reasons for an property storage rewrite.
  /// </summary>
  public enum RewriteReason
  {
    /// <summary>
    /// A previously persisted storage is loaded and needs to be migrated to the currently expected format.
    /// </summary>
    Migration,
    /// <summary>An existing storage is duplicated.</summary>
    Duplication,
  }
}
```

#### RewriteReason.Duplication
As the name suggests, a duplication is triggered when a storage is duplicated. This occurs when an existing import plan is duplicated. As a plug-in developer, you should consider which of your own configuration entries now need to be adapted. In the example, a new unique ID is generated.

#### RewriteReason.Migration
When the Auto Importer program is started and the import plans are loaded, an affected plug-in receives a migration call. The previous configuration status is provided in this call. It is up to the plug-in developer to make any necessary changes here.\
To do this, it is advisable to keep a configuration entry with the current plug-in version. This can then be compared with the current version during the migration call and the necessary settings can be migrated.\
This migration is optional, the plug-in developer has the possibility to make adjustments himself, but it can also be left to the Auto Importer, which then saves the configuration itself.

An example would be splitting hostname and port in a configuration entry: "localhost:1234" into two configuration items, hostname and port.
So that the hostname and port information is not lost, the developer can split the old entry and store it in the now separate fields without the user having to provide the information again.

To implement this, you would react to **RewriteReason.Migration** in the **RewritePropertyStorage** method. You would read the plug-in version from the *IPropertyStorage* and compare it, if necessary you could now transfer the common entry into two new entries.