#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Sdk.Import.ImportController.Exceptions;

/// <summary>
/// Represents an error when an import file is rescheduled.
/// </summary>
public class SchedulingException : ImportControllerException
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="SchedulingException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public SchedulingException( string message ) : base( message )
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="SchedulingException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">
	/// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
	/// </param>
	public SchedulingException( string message, Exception? innerException ) : base( message, innerException )
	{
	}

	#endregion
}