#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace ImportPluginBase.ImportModule;

/// <summary>
/// Responsible for running custom import logic.
/// </summary>
public interface IImportRunner : IAsyncDisposable
{
    /// <summary>
    ///	Executes a custom import automation. This method is called when an import plan is started by the user. The returned Task represents
    /// the executing import automation and should not complete until the automation is explicitly stopped by the user. When the user stops
    /// the import plan, the given cancellation token is canceled. At this point the returned task is expected to complete after a short
    /// amount of time but the current import activity should be completed beforehand. This method should be implemented asynchronous. This
    /// means that it is expected to return a task quickly and not to block the thread at any point. Use Task.Run to run synchronous
    /// blocking code on a background thread if necessary.
    /// </summary>
    public Task RunAsync(CancellationToken cancellationToken);
}