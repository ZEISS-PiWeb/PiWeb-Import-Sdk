#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2023                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

/// <summary>
/// Responsible for running custom import logic.
/// </summary>
public interface IImportRunner : IDisposable
{
	#region methods

	/// <summary>
	///	Executes a custom import automation. This method is called when an import plan is started by the user. The returned Task represents
	/// the executing import automation and should not complete until the automation is explicitly stopped by the user. When the user wants
	/// to stop the import plan, the given cancellation token is canceled. After this point the returned task is expected to complete
	/// eventually, but the current import activity should be completed beforehand. This method needs to be implemented asynchronous.
	/// This means that it is expected to return a task quickly and not to block the thread at any point.
	/// Use <see cref="Task.Run(System.Action)"/> to run synchronous blocking code on a background thread if necessary.
	/// </summary>
	Task RunAsync(CancellationToken cancellationToken = default);

	void IDisposable.Dispose()
	{
		// Empty, so it does not need to be implemented when not needed.
	}

	#endregion
}