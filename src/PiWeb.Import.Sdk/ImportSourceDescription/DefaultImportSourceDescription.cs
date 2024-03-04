#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Import.Sdk.ImportSourceDescription;

using System;
using System.Collections.Immutable;

/// <summary>
/// <inheritdoc />
/// This implementation displays the title of the import automation as import source.
/// </summary>
public sealed class DefaultImportSourceDescription : IImportSourceDescription
{
	#region constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="DefaultImportSourceDescription"/> class.
	/// </summary>
	private DefaultImportSourceDescription() {}

	#endregion

	#region properties

	/// <summary>
	/// The single instance of <see cref="IImportSourceDescription"/>.
	/// </summary>
	public static readonly IImportSourceDescription Instance = new DefaultImportSourceDescription();

	#endregion

	#region interface IImportSourceDescription

	/// <inheritdoc />
	public string Name => string.Empty;

	/// <inheritdoc />
	public ImmutableList<DescriptionProperty> DescriptionItems => ImmutableList<DescriptionProperty>.Empty;

	/// <inheritdoc />
	public event EventHandler? Changed
	{
		// This instance is immutable and so the changed event will never be raised. We can simply throw away all event registrations
		// because that does not make a difference and avoids keeping alive registered objects for no reason.

		add { }
		remove { }
	}

	#endregion
}