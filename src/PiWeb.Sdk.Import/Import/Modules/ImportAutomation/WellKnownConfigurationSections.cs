#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using Zeiss.PiWeb.Sdk.Common.ConfigurationItems;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

/// <summary>
/// Represents the list of well known configuration sections in the Auto Importer.
/// Use these values to insert custom configuration items into built-in Auto Importer sections.
/// </summary>
public static class WellKnownConfigurationSections
{
	#region members

	/// <summary>
	/// The source section.
	/// </summary>
	public static readonly Section Source = new Section { Title = "Source", Priority = -10000 };

	/// <summary>
	/// The target section.
	/// </summary>
	public static readonly Section Target = new Section { Title = "Target", Priority = -9000 };

	/// <summary>
	/// The execution section.
	/// </summary>
	public static readonly Section Execution = new Section { Title = "Execution", Priority = -8000 };

	/// <summary>
	/// The import history section.
	/// </summary>
	public static readonly Section ImportHistory = new Section { Title = "ImportHistory", Priority = -7000 };

	/// <summary>
	/// The backup section.
	/// </summary>
	public static readonly Section Backup = new Section { Title = "Backup", Priority = -6000 };

	#endregion
}