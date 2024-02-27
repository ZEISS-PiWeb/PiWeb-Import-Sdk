#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

using System.Globalization;
using System.Threading.Tasks;
using ConfigurationItems;
using PropertyStorage;

/// <summary>
/// Represents a custom import automation provided as part of a plugin. Custom import automations substitute the full Auto Importer
/// import pipeline with custom logic and are available as import sources in an import plan configuration.
/// </summary>
public interface IImportAutomation
{
	#region methods

	/// <summary>
	/// Creates a new import runner instance. An import runner is first created and then executed when an import plan using this import
	/// automation as import source is started. Each import plan is expected to use a separate import runner instance. For this reason
	/// this method must never return the same <see cref="IImportRunner"/> instance twice.
	/// If the import runner cannot be created (e.g. because of invalid import plan settings), a <see cref="CreateImportRunnerException"/>
	/// can be thrown. The created instance will be disposed after the import plan is stopped.
	/// </summary>
	/// <param name="context">
	/// The context for the new import runner instance. It contains information about the import plan to execute and allows to post import
	/// events and status updates.
	/// </param>
	/// <returns>The new import runner.</returns>
	/// <exception cref="CreateImportRunnerException">
	/// Thrown when the import runner cannot be created. Throwing this will abort the execution of the import plan but it will not post
	/// any error events. It is therefore recommended to post an error event before throwing this exception.
	/// </exception>
	Task<IImportRunner> CreateImportRunnerAsync( IImportRunnerContext context );

	/// <summary>
	/// Creates a new automation configuration instance. An automation configuration is first created when an import plan uses this
	/// import automation as import source. Each import plan is expected to use a separate automation configuration instance.
	/// For this reason this method must never return the same <see cref="IAutomationConfiguration"/> instance twice.
	/// </summary>
	/// <param name="context">
	/// The context for the new automation configuration instance. It contains information about the configuration of an import automation
	/// for a specific import plan.
	/// </param>
	/// <returns>The new automation configuration.</returns>
	IAutomationConfiguration CreateConfiguration( IAutomationConfigurationContext context )
	{
		return NullAutomationConfiguration.Instance;
	}

	/// <summary>
	/// This method is called when an existing property storage needs to be rewritten. Rewriting a property storage is possible in two
	/// scenarios: after loading a previously persisted property storage and after duplicating an existing property storage.
	/// The actual reason for the rewrite is specified by the <see cref="IRewriteContext.RewriteReason"/> property of the context parameter.
	/// In the case of loading a previously persisted property storage, the rewrite aims to transform the contents of the storage to
	/// the expected format of an <see cref="IImportRunner"/> or of an <see cref="IAutomationConfiguration"/> instance because the storage
	/// was potentially persisted by an older version of this import automation using a different configuration format.
	/// In the case of duplicating an existing property storage, the rewrite can ensure invariants on the data in the storage.
	/// For example a property that should always be unique over all import plans (usually UUIDs, temp file paths or similar).
	/// </summary>
	/// <param name="propertyStorage">The property storage to update.</param>
	/// <param name="context">A context instance which holds additional information about the operation.</param>
	void RewritePropertyStorage( IPropertyStorage propertyStorage, IRewriteContext context )
	{
		// Do nothing when not overwritten
	}

	/// <summary>
	/// This method can be implemented to define a custom localization of event and status messages or format strings in the
	/// Auto Importer UI. When this method returns <c>null</c>, the built-in localization is attempted. It is not necessary to
	/// implement this method when only the built-in localization is used.
	/// </summary>
	/// <param name="localizationCulture">Specifies the target language.</param>
	/// <param name="text">The text or format string to translate.</param>
	/// <returns>The localized text or <c>null</c> when the built-in localization should be queried.</returns>
	Task<string?> LocalizeText( CultureInfo localizationCulture, string text )
	{
		return Task.FromResult<string?>( null );
	}

	/// <summary>
	/// This method can be implemented to define a custom formatting of format strings in the Auto Importer UI. When this method returns
	/// <c>null</c>, the built-in formatting is attempted. It is not necessary to implement this method when only the built-in formatting
	/// is used.
	/// </summary>
	/// <param name="formatCulture">Specifies how to format arguments.</param>
	/// <param name="text">The text text to format.</param>
	/// <param name="args">The arguments to use for formatting.</param>
	/// <returns>The formatted text or <c>null</c> when the built-in formatting should be queried.</returns>
	Task<string?> FormatText( CultureInfo formatCulture, string text, params object[] args )
	{
		return Task.FromResult<string?>( null );
	}

	#endregion
}