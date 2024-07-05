---
layout: default
nav_order: 7
parent: Plugin fundamentals
title: Import monitoring
---

# {{ page.title }}

<!---
Ziele:
- aufzeigen, wie Monitoring der Ausführung des Plugins möglich ist

Inhalt:
- Logging
- Status
- Events
--->

```c#
public Task Init(IPluginContext context)
{
    var logger = context.Logger;

    logger.LogTrace("Plugin test message");
    logger.LogDebug("Plugin test message");
    logger.LogInformation("Plugin test message");
    logger.LogWarning("Plugin test message with parameter: {parameter}", 5);
    logger.LogError(new Exception("Test exception"), "Plugin test message");
    logger.LogInformation("Current UI culture: {cultureName}", CultureInfo.CurrentUICulture.Name);

    context.RegisterImportAutomation("MyImportModule", new MyImportModule());

    return Task.CompletedTask;
}
```