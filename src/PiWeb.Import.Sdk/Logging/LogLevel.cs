#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Logging;

/// <summary>
/// Defines log levels.
/// </summary>
public enum LogLevel
{
	/// <summary>
	/// Used for messages that are generated very often or contain sensitive data. This level is usually not logged when running in a
	/// production environment and will only be explicitly enabled when analyzing a misbehavior.
	/// </summary>
	Trace = 0,

	/// <summary>
	/// Used for messages that are useful during development.
	/// </summary>
	Debug = 1,

	/// <summary>
	/// Used for messages that track the normal flow of execution.
	/// </summary>
	Information = 2,

	/// <summary>
	/// Used for messages that show unexpected results or behavior in the normal flow of execution.
	/// </summary>
	Warning = 3,

	/// <summary>
	/// Used for messages that show failures during the normal flow of execution.
	/// </summary>
	Error = 4
}