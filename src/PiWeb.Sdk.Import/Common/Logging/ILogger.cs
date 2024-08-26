#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Sdk.Common.Logging;

/// <summary>
/// Responsible for logging messages.
/// </summary>
public interface ILogger
{
	#region methods

	/// <summary>
	/// Checks if the given <paramref name="logLevel"/> is enabled.
	/// </summary>
	/// <param name="logLevel">Level to be checked.</param>
	/// <returns><c>true</c> if enabled.</returns>
	bool IsEnabled( LogLevel logLevel );

	/// <summary>
	/// Logs a message with the given log level.
	/// </summary>
	/// <param name="level">The log level of the message.</param>
	/// <param name="ex">The exception to attach to the log message. If this parameter is <c>null</c>, no exception is attached.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	void Log( LogLevel level, Exception? ex, string message, params object?[] args );

	#endregion
}