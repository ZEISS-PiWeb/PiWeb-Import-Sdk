#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ConfigurationItems;

using Modules.ImportAutomation;

/// <summary>
/// <inheritdoc />
/// This implementation does not specify any configuration.
/// </summary>
public sealed class NullAutomationConfiguration : IAutomationConfiguration
{
	#region members

	/// <summary>
	/// Gets the instance of the <see cref="NullAutomationConfiguration"/> class.
	/// </summary>
	public static readonly IAutomationConfiguration Instance = new NullAutomationConfiguration();

	#endregion

	#region constructors

	private NullAutomationConfiguration()
	{ }

	#endregion
}