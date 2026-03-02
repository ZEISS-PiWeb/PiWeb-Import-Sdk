#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2026                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.ImportController.Exceptions;

/// <summary>
/// Represents an error where rescheduling of an import file fails be rescheduling is not supported.
/// </summary>
public sealed class SchedulingNotSupportedException : SchedulingException
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="SchedulingNotSupportedException"/> class.
	/// </summary>
	public SchedulingNotSupportedException() : base( "Scheduling is not supported." )
	{ }

	#endregion
}