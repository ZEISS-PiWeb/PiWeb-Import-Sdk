#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

using System;
using ConfigurationItems;
using ImportSourceDescription;

/// <summary>
/// <inheritdoc />
/// This base class provides a comfortable implementation for modifiable source descriptions accessible via the protected
/// <see cref="ImportSourceDescription"/> property.
/// </summary>
public class AutomationConfigurationBase : IAutomationConfiguration
{
	#region properties

	/// <summary>
	/// Provides a modifiable import source description with change notification. This implementation is thread safe and can be modified
	/// at any point in time. Usually it is initialized and updated in the <see cref="IAutomationConfiguration.Update"/> implementation to
	/// show current settings as import source properties.
	/// </summary>
	protected EditableImportSourceDescription ImportSourceDescription { get; } = new EditableImportSourceDescription();

	#endregion

	#region methods

	/// <inheritdoc cref="IDisposable" />
	protected virtual void Dispose( bool disposing )
	{
	}

	#endregion

	#region interface IImportConfiguration

	/// <inheritdoc />
	public virtual void Update()
	{
		// Empty, so it does not need to be implemented when not needed.
	}

	/// <inheritdoc />
	public virtual IImportSourceDescription GetImportSourceDescription()
	{
		return ImportSourceDescription;
	}

	/// <inheritdoc />
	public void Dispose()
	{
		Dispose( true );
		GC.SuppressFinalize( this );
	}

	#endregion
}