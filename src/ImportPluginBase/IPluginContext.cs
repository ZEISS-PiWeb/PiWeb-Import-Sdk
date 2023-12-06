#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace ImportPluginBase;

#region usings

using Environment;
using Exceptions;
using ImportModule;
using Logging;

#endregion

/// <summary>
/// Represents the context of a plugin provided by the hosting application.
/// </summary>
public interface IPluginContext
{
    #region properties

    /// <summary>
    /// Contains information about the environment a plugin is hosted in.
    /// </summary>
    public IEnvironmentInfo EnvironmentInfo { get; }

    /// <summary>
    /// A logger that can be used to write log entries. Written entries are usually forwarded to the log file of the hosting application.
    /// </summary>
    public ILogger Logger { get; }

    /// <summary>
    /// Registers an import module.
    /// </summary>
    /// <param name="id">The id of the module.</param>
    /// <param name="importModule">The import module to register.</param>
    /// <exception cref="ModuleRegistrationException">Thrown when the import module cannot be registered.</exception>
    public void RegisterImportModule(string id, IImportModule importModule);

    #endregion
}