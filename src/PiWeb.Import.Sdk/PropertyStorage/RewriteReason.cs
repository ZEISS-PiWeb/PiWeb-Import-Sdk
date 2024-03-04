#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.PropertyStorage;

/// <summary>
/// Enumerates possible reasons for an property storage rewrite.
/// </summary>
public enum RewriteReason
{
	/// <summary>
	/// A previously persisted storage is loaded and needs to be migrated to the currently expected format.
	/// </summary>
	Migration,

	/// <summary>
	/// An existing storage is duplicated.
	/// </summary>
	Duplication
}