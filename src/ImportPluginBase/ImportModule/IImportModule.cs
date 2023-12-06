#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace ImportPluginBase.ImportModule;

/// <summary>
/// Represents an import module. Import modules substitute the full Auto Importer import pipeline with custom logic.
/// </summary>
public interface IImportModule
{
    /// <summary>
    /// Creates a new import runner instance. The import runner is created and started when an import plan using this import module is
    /// started. Each import plan uses its own instance of an import runner to execute.
    /// </summary>
    /// <param name="context">
    /// The context of the import runner to create. It contains information about the import plan to execute and allows to communicate
    /// events and status updates.
    /// </param>
    /// <returns>The new import runner.</returns>
    Task<IImportRunner> CreateImportRunnerAsync(IImportRunnerContext context);
}