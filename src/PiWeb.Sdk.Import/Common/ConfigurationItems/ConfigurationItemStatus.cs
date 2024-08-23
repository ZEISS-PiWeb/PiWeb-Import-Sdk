#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

/// <summary>
/// Possible states of validness of a <see cref="IConfigurationItem"/>.
/// </summary>
public enum ConfigurationItemStatus
{
	/// <summary>
	/// The <see cref="IConfigurationItem"/> is valid.
	/// </summary>
	Valid,

	/// <summary>
	/// The <see cref="IConfigurationItem"/> is invalid. This will prevent the import plan to run.
	/// </summary>
	Invalid
}