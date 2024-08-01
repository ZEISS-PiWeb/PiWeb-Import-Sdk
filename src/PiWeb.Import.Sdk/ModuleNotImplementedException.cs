#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;

namespace Zeiss.PiWeb.Import.Sdk;

/// <summary>
/// Represents an error during module instantiation.
/// </summary>
public class ModuleNotImplementedException : PluginException
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="ModuleNotImplementedException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public ModuleNotImplementedException( string message ) : base( message )
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="ModuleNotImplementedException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">
	/// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
	/// </param>
	public ModuleNotImplementedException( string message, Exception? innerException ) : base( message, innerException )
	{
	}

	#endregion
}