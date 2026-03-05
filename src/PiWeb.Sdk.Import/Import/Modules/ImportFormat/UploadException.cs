#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using Zeiss.PiWeb.Sdk.Common.Exceptions;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

/// <summary>
/// Represents an error during import data upload.
/// </summary>
public class UploadException : PluginException
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="UploadException"/> class.
	/// </summary>
	public UploadException()
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="UploadException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public UploadException( string message ) : base( message )
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="UploadException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">
	/// The exception that is the cause of the current exception, or a null reference if no inner exception
	/// is specified.
	/// </param>
	public UploadException( string message, Exception? innerException ) : base( message, innerException )
	{ }

	#endregion
}