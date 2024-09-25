#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportPlan;

/// <summary>
/// Holds execution specific properties.
/// </summary>
public record Execution
{
	#region properties

	/// <summary>
	/// The execution mode of the associated import plan, e.g. In process or as service.
	/// </summary>
	public required ExecutionMode Mode { get; init; }

	/// <summary>
	/// Indicates whether the import plan is run automatically after user login.
	/// </summary>
	public required bool Autostart { get; init; }

	#endregion
}