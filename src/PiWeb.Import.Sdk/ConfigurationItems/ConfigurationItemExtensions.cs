#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

/// <summary>
/// Responsible to provide extensions for <see cref="ConfigurationItemBase"/>.
/// </summary>
public static class ConfigurationItemExtensions
{
	#region methods

	/// <summary>
	/// Configures the <see cref="IConfigurationItem"/> to be in an error state.
	/// </summary >
	/// <param name="configurationItem">The <see cref="IConfigurationItem"/> to configure.</param>
	/// <param name="errorMessage">The error message that explains the status.</param>
	/// <param name="setInvalid">
	/// <c>true</c> when the <see cref="IConfigurationItem"/> should have <see cref="ConfigurationItemStatus.Invalid"/>.
	/// </param>
	public static void SetError( this ConfigurationItemBase configurationItem, string errorMessage, bool setInvalid = true )
	{
		configurationItem.Status = setInvalid ? ConfigurationItemStatus.Invalid : ConfigurationItemStatus.Valid;
		configurationItem.Notification = new ConfigurationItemNotification( NotificationType.Error, errorMessage );
	}

	/// <summary>
	/// Configures the <see cref="IConfigurationItem"/> to be in an warning state.
	/// </summary >
	/// <param name="configurationItem">The <see cref="IConfigurationItem"/> to configure.</param>
	/// <param name="warnMessage">The warning message that explains the status.</param>
	/// <param name="setInvalid">
	/// <c>true</c> when the <see cref="IConfigurationItem"/> should have <see cref="ConfigurationItemStatus.Invalid"/>.
	/// </param>
	public static void SetWarning( this ConfigurationItemBase configurationItem, string? warnMessage = null, bool setInvalid = false )
	{
		configurationItem.Status = setInvalid ? ConfigurationItemStatus.Invalid : ConfigurationItemStatus.Valid;
		configurationItem.Notification = new ConfigurationItemNotification( NotificationType.Warning, warnMessage );
	}

	/// <summary>
	/// Sets the status of the <see cref="IConfigurationItem"/> to <see cref="NotificationType.None"/>.
	/// </summary >
	/// <param name="configurationItem">The <see cref="IConfigurationItem"/> to configure.</param>
	public static void ClearStatus( this ConfigurationItemBase configurationItem )
	{
		configurationItem.Status = ConfigurationItemStatus.Valid;
		configurationItem.Notification = ConfigurationItemNotification.None;
	}

	#endregion
}