#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.Modules.ImportAutomation;

using System;

/// <summary>
/// Represents an error during <see cref="IImportRunner"/> instance creation.
/// </summary>
public class CreateImportRunnerException : PluginException
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateImportRunnerException"/> class.
	/// </summary>
	public CreateImportRunnerException()
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateImportRunnerException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public CreateImportRunnerException( string message ) : base( message )
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateImportRunnerException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">
	/// The exception that is the cause of the current exception, or a null reference if no inner exception
	/// is specified.
	/// </param>
	public CreateImportRunnerException( string message, Exception? innerException ) : base( message, innerException )
	{ }

	#endregion
}