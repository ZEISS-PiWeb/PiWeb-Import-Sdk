#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.Sdk.Common.Environment;

/// <summary>
/// Represents various information about the environment a plugin is hosted in.
/// </summary>
public interface IEnvironmentInfo
{
	#region properties

	/// <summary>
	/// The name of the application hosting the plugin. This should only be used for display or to work around known
	/// application specific bugs.
	/// </summary>
	string AppName { get; }

	/// <summary>
	/// The version of the application hosting the plugin. The form of this version string is specific to the hosting
	/// application and therefore has no guaranteed form. Use this for display or to work around known application
	/// specific bugs.
	/// </summary>
	string AppVersion { get; }

	/// <summary>
	/// The type of the application hosting the plugin.
	/// </summary>
	HostType HostType { get; }

	/// <summary>
	/// The version of the plugin api provided by the hosting application. Follows Semantic Versioning 2.0.0.
	/// See https://semver.org/spec/v2.0.0.html.
	/// </summary>
	string ApiVersion { get; }

	#endregion
}