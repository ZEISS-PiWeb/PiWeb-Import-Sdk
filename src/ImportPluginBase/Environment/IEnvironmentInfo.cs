#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace ImportPluginBase.Environment;

/// <summary>
/// Represents various information about the environment a plugin is hosted in.
/// </summary>
public interface IEnvironmentInfo
{
    #region properties

    /// <summary>
    /// The name of the application hosting the plugin. This should only be used for display or to work around known application
    /// specific bugs.
    /// </summary>
    public string AppName { get; }

    /// <summary>
    /// The version of the application hosting the plugin. This has no guaranteed form and should only be used for display or
    /// to work around known application specific bugs.
    /// </summary>
    public string AppVersion { get; }

    /// <summary>
    /// The type of the application hosting the plugin.
    /// </summary>
    public HostType HostType { get; }

    /// <summary>
    /// The version of the plugin api provided by the hosting application. Follows Semantic Versioning 2.0.0.
    /// See https://semver.org/lang/de/spec/v2.0.0.html.
    /// </summary>
    public string ApiVersion { get; }

    #endregion
}