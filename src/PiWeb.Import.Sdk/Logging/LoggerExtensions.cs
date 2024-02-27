#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Logging;

using System;

/// <summary>
/// A collection of extension methods for <see cref="ILogger"/>.
/// </summary>
public static class LoggerExtensions
{
	#region methods

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Trace"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogTrace( this ILogger logger, string message, params object?[] args )
	{
		logger.Log( LogLevel.Trace, null, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Trace"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="ex">The exception to attach to the log message. If this parameter is <c>null</c>, no exception is attached.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogTrace( this ILogger logger, Exception? ex, string message, params object?[] args )
	{
		logger.Log( LogLevel.Trace, ex, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Debug"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogDebug( this ILogger logger, string message, params object?[] args )
	{
		logger.Log( LogLevel.Debug, null, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Debug"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="ex">The exception to attach to the log message. If this parameter is <c>null</c>, no exception is attached.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogDebug( this ILogger logger, Exception? ex, string message, params object?[] args )
	{
		logger.Log( LogLevel.Debug, ex, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Information"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogInformation( this ILogger logger, string message, params object?[] args )
	{
		logger.Log( LogLevel.Information, null, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Information"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="ex">The exception to attach to the log message. If this parameter is <c>null</c>, no exception is attached.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogInformation( this ILogger logger, Exception? ex, string message, params object?[] args )
	{
		logger.Log( LogLevel.Information, ex, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Warning"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogWarning( this ILogger logger, string message, params object?[] args )
	{
		logger.Log( LogLevel.Warning, null, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Warning"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="ex">The exception to attach to the log message. If this parameter is <c>null</c>, no exception is attached.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogWarning( this ILogger logger, Exception? ex, string message, params object?[] args )
	{
		logger.Log( LogLevel.Warning, ex, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Error"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogError( this ILogger logger, string message, params object?[] args )
	{
		logger.Log( LogLevel.Error, null, message, args );
	}

	/// <summary>
	/// Logs a message with <see cref="LogLevel.Error"/> level.
	/// </summary>
	/// <param name="logger">The logger to log the message with.</param>
	/// <param name="ex">The exception to attach to the log message. If this parameter is <c>null</c>, no exception is attached.</param>
	/// <param name="message">The message to log.</param>
	/// <param name="args">An array of objects that can be formatted as part of the message.</param>
	public static void LogError( this ILogger logger, Exception? ex, string message, params object?[] args )
	{
		logger.Log( LogLevel.Error, ex, message, args );
	}

	#endregion
}