#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

/// <summary>
/// <inheritdoc />
/// This implementation does not specify any configuration.
/// </summary>
public sealed class DefaultAutomationConfiguration : IAutomationConfiguration
{
	#region members

	/// <summary>
	/// Gets the instance of the <see cref="DefaultAutomationConfiguration"/> class.
	/// </summary>
	public static readonly IAutomationConfiguration Instance = new DefaultAutomationConfiguration();

	#endregion

	#region constructors

	private DefaultAutomationConfiguration()
	{ }

	#endregion
}