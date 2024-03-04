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
/// <inheritdoc />
/// This implementation ignores any logging request and does not log anything.
/// </summary>
public sealed class NullLogger : ILogger
{
	#region interface ILogger

	/// <inheritdoc />
	public bool IsEnabled( LogLevel logLevel )
	{
		return false;
	}

	/// <inheritdoc />
	public void Log( LogLevel level, Exception? ex, string message, params object?[] args )
	{
		// Does not log anything.
	}

	#endregion
}