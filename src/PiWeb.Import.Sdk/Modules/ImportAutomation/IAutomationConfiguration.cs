#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

using System;

/// <summary>
/// Represents an automation configuration. Automation configurations specify additional settings for an import automation a user
/// can edit to affect the import automation behavior for an import plan.
/// </summary>
public interface IAutomationConfiguration : IDisposable
{
	#region methods

	/// <summary>
	/// Method that is invoked after any update to the import plan and once after the <see cref="IAutomationConfiguration"/>
	/// was initialized.
	/// </summary>
	/// <remarks>
	/// This method can be used to update the internal state (e.g. visible) of the defined configuration items and also to update
	/// the import source properties displayed in the hosting application.
	/// </remarks>
	void Update()
	{
		// Empty, so it does not need to be implemented when not needed.
	}

	/// <inheritdoc />
	void IDisposable.Dispose()
	{
		// Empty, so it does not need to be implemented when not needed.
	}
	
	#endregion
}