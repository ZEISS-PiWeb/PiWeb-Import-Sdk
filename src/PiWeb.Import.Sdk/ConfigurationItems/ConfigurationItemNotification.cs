#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

/// <summary>
/// Represents the notification of a configuration item.
/// </summary>
public record ConfigurationItemNotification
{
	#region members

	/// <summary>
	/// The notification representing that there is no notification.
	/// </summary>
	public static readonly ConfigurationItemNotification None = new ConfigurationItemNotification( NotificationType.None );

	#endregion

	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationItemNotification"/> class.
	/// </summary>
	/// <param name="type">The type of notification.</param>
	/// <param name="message">The optional message.</param>
	public ConfigurationItemNotification( NotificationType type, string? message = null )
	{
		Message = message;
		Type = type;
	}

	#endregion

	#region properties

	/// <summary>
	/// The type of notification.
	/// </summary>
	public NotificationType Type { get; }

	/// <summary>
	/// The notification message, when there is one. This might be displayed only when <see cref="Type"/> is
	/// <see cref="NotificationType.Error"/> or <see cref="NotificationType.Warning"/>.
	/// </summary>
	public string? Message { get; }

	#endregion
}