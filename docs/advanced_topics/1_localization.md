---
layout: default
nav_order: 1
parent: Advanced topics
title: Localization
---

<!---
Ziele:
- Mechanismus für Lokalisierung beschreiben

Inhalt:
- Übersetzung der Informationen im Manifest
- allgemeine Herangehensweise zur Lokalisierung von UI-Elementen beschreiben
--->

# {{ page.title }}
The Import SDK supports multilingual plug-ins, the content of the manifest as well as log, activities and status entries can be localized. This article deals with the use of this feature.

## Manifest localization
The plug-in system supports localization of the manifest file, in which the supported language abbreviations appear as additional subfolders in the "locals" subfolder and contain a manifest.json.\
![Localization](../../assets/images/plugin_fundamentals/1_localization.png "Localization")

Example content of a localized manifest.json insides `locales\en-US\` folder:

```json
{
  "$schema": "https://raw.githubusercontent.com/ZEISS-PiWeb/PiWeb-Import-Sdk/refs/heads/pub/schemas/manifest-localization-schema.json",
  "translation": {
    "title": "My plug-in",
    "description": "Shows how an import source can be provided by a plug-in.",
    "importAutomation.displayName": "My import automation",
    "importAutomation.summary": "This import automation provides its own import source."
  }  
}
```

The localization files use a name value schema, i.e. the entire structure does not have to be rebuilt in the same way as the original json. The following schema is used for this.

```json
{
  "$schema": "http://json-schema.org/draft-07/schema",
  "$id": "PiWebImportPluginManifestLocalizationSchema",
  "title": "PiWeb import plug-in manifest localization schema",
  "description": "Describes the localization file for a PiWeb import plug-in manifest.",
  "type": "object",
  "properties": {
    "$schema": {
      "title": "Schema",
      "description": "Pointer to the schema against which this document should be validated.",
      "type": "string"
    },
    "translation": {
      "type": "object",
      "description": "The list of localizations for the plug-in.",
      "properties": {
        "title": {
          "description": "The localization for the plug-in title.",
          "type": "string"
        },
        "description": {
          "description": "The localization for the plug-in description.",
          "type": "string"
        },
        "homepage": {
          "description": "The localization for the plug-in homepage address. This can be used to provide language specific homepage URLs if necessary.",
          "type": "string",
          "format": "uri-reference"
        },
        "contact": {
          "description": "The localization for the plug-in contact. This can be used to provide language specific contact addresses.",
          "type": "string",
          "format": "email"
        },
        "documentation": {
          "description": "The localization for the plug-in documentation address. This can be used to provide language specific documentation URLs if necessary.",
          "type": "string",
          "format": "uri-reference"
        },
        "provides.displayName": {
          "description": "The localization for the display name of the provided import automation or import format.",
          "type": "string"
        },
        "provides.summary": {
          "description": "The localization for the summary of the provided import automation.",
          "type": "string"
        }
      }
    }
  }
}
```

## ILocalizationHandler
You can use an implementation of the interface `ILocalizationHandler` to make your own translations. These translations are currently supported in the `IActivityService` by the `PostImportEvent` and `SetActivity` methods and in the `IImportHistoryService` interface for the `AddMessage` method.

{: .note }
These messages are stored semantically and translated into the current language when the application is started. This language does not have to be the language in which the message was created. If the required plug-in is no longer available, there is a fallback to a defined message (usually in English).

```c#
public class LocalizationHandler : ILocalizationHandler
{
    public string LocalizeAndFormatText(string text, object[] args, ILocalizationContext context)
    {
        return string.Format(
            context.FormatCulture,
            LocalizeText(text, context.TranslationCulture),
            args);
    }

    private static string LocalizeText(string text, CultureInfo translationCulture)
    {
        if (translationCulture.TwoLetterISOLanguageName == "de")
        {
            return text switch
            {
                "Info" => "Eine Info-Level-Nachricht",
                "InfoWithArg" => "Eine Info-Level Nachricht mit Argument {0}",
                "Error" => "Eine Error-Level-Nachricht",
                "ErrorWithArg" => "Eine Error-Level-Nachricht mit Argument {0}",
                _ => $"#{text}"
            };
        }

        return text switch
        {
            "Info" => "Some info level message",
            "InfoWithArg" => "Some info level message with argument {0}",
            "Error" => "Some error level message",
            "ErrorWithArg" => "Some error level message with argument {0}",
            _ => $"#{text}"
        };
    }
}
```

In the implementation of `IPlugin`, the custom handler is registered via `CreateLocalizationHandler`.

```c#
public class MyPlugin : IPlugin
{
  public IImportAutomation CreateImportAutomation(ICreateImportAutomationContext context)
  {
    return new MyImportAutomation();
  }

  public ILocalizationHandler CreateLocalizationHandler(ICreateLocalizationHandlerContext context)
  {
      return new LocalizationHandler();
  }
}
```

The `LocalizationHandler` is used automatically when the `PostImportEvent` of the StatusService is called, for example.

```c#
public sealed class MyImportRunner : IImportRunner
{
    private readonly IActivityService _ActivityService;

    public MyImportRunner(ICreateImportRunnerContext context)
    {
        _ActivityService = context.StatusService;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            _ActivityService.PostImportEvent(EventSeverity.Info, "Info");
            _ActivityService.PostImportEvent(EventSeverity.Info, "InfoWithArg", 1);

            _ActivityService.PostImportEvent(EventSeverity.Error, "Error");
            _ActivityService.PostImportEvent(EventSeverity.Error, "ErrorWithArg", 1);

            await Task.Delay(TimeSpan.FromMilliseconds(-1), cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // Do nothing
        }
    }
}
```

## Custom texts and future
Further translations could take place via a separate implementation, e.g. within the `ILocalizationHandler`, and could be controlled via a JSON file or similar.\
In the future, it is planned that the Import SDK will offer further possibilities for translation, similar to the manifest translation.