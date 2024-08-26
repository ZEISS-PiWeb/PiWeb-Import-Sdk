#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

/// <summary>
/// Enumerates all possible types of import activities.
/// </summary>
public enum ActivityType
{
	/// <summary>
	/// Normal activity, has no special behavior.
	/// </summary>
	Normal,

	/// <summary>
	/// Indicates that the import automation is temporarily suspended. This means that no imports are currently being carried out.
	/// An activity of this type should be set after an error occurs when time is required to recover from this error. A typical example
	/// is when the import target server cannot be reached. In this situation, it is recommended to wait for a short period of time
	/// before trying again to avoid traffic. Use a suspension activity during this wait time.
	/// </summary>
	Suspension
}