#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportPlan;

/// <summary>
/// Represents the different states an import plan runtime can be in.
/// </summary>
public enum RunState
{
	/// <summary>
	/// The runtime is stopped.
	/// </summary>
	Stopped,

	/// <summary>
	/// The runtime is starting.
	/// </summary>
	Starting,

	/// <summary>
	/// The runtime is running.
	/// </summary>
	Running,

	/// <summary>
	/// The runtime is stopping.
	/// </summary>
	Stopping,

	/// <summary>
	/// The Windows service associated with the runtime is installed and stopped.
	/// </summary>
	Installed
}