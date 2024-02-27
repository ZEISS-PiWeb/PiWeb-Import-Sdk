#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk;

using System;

/// <summary>
/// Represents an error during module registration.
/// </summary>
public class ModuleRegistrationException : PluginException
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="ModuleRegistrationException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="moduleId">The module id.</param>
	public ModuleRegistrationException( string message, string moduleId ) : base( message )
	{
		ModuleId = moduleId;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ModuleRegistrationException"/> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="moduleId">The module id.</param>
	/// <param name="innerException">
	/// The exception that is the cause of the current exception, or a null reference if no inner exception
	/// is specified.
	/// </param>
	public ModuleRegistrationException( string message, string moduleId, Exception? innerException ) : base( message, innerException )
	{
		ModuleId = moduleId;
	}

	#endregion

	#region properties

	/// <summary>
	/// The module id.
	/// </summary>
	public string ModuleId { get; }

	#endregion
}