#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

/// <summary>
/// Contains constants for priority values of the various built-in configuration items. These constants can be used as base values to
/// define priorities for custom configuration items. Priorities are used to order custom and built-in configuration items in the UI.
/// Configuration items with lower priority values are placed above configuration items with higher priority values.
/// </summary>
/// <remarks>
/// Some examples use cases for priorities:
/// <br/>
/// <br/>
///
/// <b>Placing a configuration item before a built-in setting:</b>
/// <br/>
/// <code>
/// new BoolConfigurationItem
/// {
///		Category = WellKnownSections.Target,
///		Title = "Item before Server Connection",
///		Priority = WellKnownPriorities.Target.ServerConnection - 1
/// };
/// </code>
/// <br/>
///
/// <b>Placing a configuration item after a built-in setting:</b>
/// <br/>
/// <code>
/// new BoolConfigurationItem
/// {
///		Category = WellKnownSections.Target,
///		Title = "Item after Server Connection",
///		Priority = WellKnownPriorities.Target.ServerConnection + 1
/// };
/// </code>
/// <br/>
///
/// <b>Placing a configuration item before any built-in setting:</b>
/// <br/>
/// <code>
/// new BoolConfigurationItem
/// {
///		Category = %ANY CATEGORY%,
///		Title = "Top most item",
///		Priority = WellKnownPriorities.TopMost
/// };
/// </code>
/// <br/>
///
/// <b>Placing a configuration item after any built-in setting:</b>
/// <br/>
/// <code>
/// new BoolConfigurationItem
/// {
///		Category = %ANY CATEGORY%,
///		Title = "Item after built-in settings",
///		// Priority = 0; // 0 is default and will always be after any built-in setting
/// };
/// </code>
/// </remarks>
public static class WellKnownConfigurationPriorities
{
	#region constants

	/// <summary>
	/// The highest priority that is still always lower then any built-in configuration item. Configuration items using this or a lower
	/// priority will be displayed above all built-in configuration items.
	/// </summary>
	public const int TopMost = -1_000_000;

	/// <summary>
	/// The lowest priority that is still always higher then any built-in configuration item. Configuration items using this or a higher
	/// priority will be displayed below all built-in configuration items.
	/// </summary>
	public const int BottomMost = 1_000_000;

	#endregion

	#region class Backup

	/// <summary>
	/// The backup section.
	/// </summary>
	public static class Backup
	{
		#region constants

		/// <summary>
		/// The description text displayed at the top of the backup section.
		/// </summary>
		public const int BackupDescription = -10_000;

		/// <summary>
		/// The backup folder selection for successful imports.
		/// </summary>
		public const int BackupAfterSuccess = -9_000;

		/// <summary>
		/// The backup folder selection for unsuccessful imports.
		/// </summary>
		public const int BackupAfterFailure = -8_000;

		#endregion
	}

	#endregion

	#region class Execution

	/// <summary>
	/// The execution section.
	/// </summary>
	public static class Execution
	{
		#region constants

		/// <summary>
		/// The execution mode selection for the import plan.
		/// </summary>
		public const int ExecutionMode = -10000;

		/// <summary>
		/// The autostart setting.
		/// </summary>
		public const int Autostart = -9000;

		#endregion
	}

	#endregion

	#region class ImportHistory

	/// <summary>
	/// The import history section.
	/// </summary>
	public static class ImportHistory
	{
		#region constants

		/// <summary>
		/// The setting for the maximum number of local log entries.
		/// </summary>
		public const int MaxOfflineLogEntries = -10000;

		/// <summary>
		/// The setting for the maximum number of log entries on the server.
		/// </summary>
		public const int MaxOnlineLogEntries = -9000;

		/// <summary>
		/// The log level selection specifying when the original import data should be attached to log entries in the import history.
		/// </summary>
		public const int AttachImportFiles = -8000;

		#endregion
	}

	#endregion

	#region class Source

	/// <summary>
	/// The source section.
	/// </summary>
	public static class Source
	{
		#region constants

		/// <summary>
		/// The import source selection.
		/// </summary>
		public const int ImportSource = -10000;

		/// <summary>
		/// The import folder selection.
		/// </summary>
		public const int ImportFolder = -9000;

		/// <summary>
		/// The expected folder structure setting.
		/// </summary>
		public const int FolderStructure = -8000;

		/// <summary>
		/// The setting specifying whether folders are created only for parts with explicitly defined import options.
		/// </summary>
		public const int PartsWithImportOptionsOnly = -7000;

		/// <summary>
		/// The setting specifying whether missing folders defined in the profile configuration should be created.
		/// </summary>
		public const int CreateFolders = -6000;

		/// <summary>
		/// The setting specifying whether existing folders not defined in the profile configuration should be removed.
		/// </summary>
		public const int DeleteFolders = -5000;

		/// <summary>
		/// The synchronisation interval setting.
		/// </summary>
		public const int SynchronizationInterval = -4000;

		#endregion
	}

	#endregion

	#region class Target

	/// <summary>
	/// The target section.
	/// </summary>
	public static class Target
	{
		#region constants

		/// <summary>
		/// The selection of the connection to the import target.
		/// </summary>
		public const int Connection = -10000;

		/// <summary>
		/// The authentication credentials setting.
		/// </summary>
		public const int AuthenticationCredentials = -9000;

		/// <summary>
		/// The connection status display.
		/// </summary>
		public const int ConnectionStatus = -8000;

		/// <summary>
		/// The separator between connection settings and the configuration of import formats.
		/// </summary>
		public const int ConnectionSeparator = -7000;

		/// <summary>
		/// The button to configure import formats.
		/// </summary>
		public const int ConfigureImportFormats = -6000;

		#endregion
	}

	#endregion
}