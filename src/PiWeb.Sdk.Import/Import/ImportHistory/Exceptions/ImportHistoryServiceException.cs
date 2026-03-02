#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using Zeiss.PiWeb.Sdk.Common.Exceptions;

namespace Zeiss.PiWeb.Sdk.Import.ImportHistory.Exceptions;

/// <summary>
/// Represents an error when using an import history service.
/// </summary>
public class ImportHistoryServiceException : PluginException
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="ImportHistoryServiceException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public ImportHistoryServiceException( string message ) : base( message )
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="ImportHistoryServiceException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">
	/// The exception that is the cause of the current exception, or a null reference if no inner exception is specified.
	/// </param>
	public ImportHistoryServiceException( string message, Exception? innerException ) : base( message, innerException )
	{
	}

	#endregion
}