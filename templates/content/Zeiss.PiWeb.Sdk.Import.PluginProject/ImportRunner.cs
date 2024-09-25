using System;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.PiWeb.Sdk.Import.PluginProject;

public class ImportRunner : IImportRunner
{
    private readonly ICreateImportRunnerContext _Context;

    public ImportRunner(ICreateImportRunnerContext context)
    {
        _Context = context;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // import loop
                await Task.Delay( TimeSpan.FromSeconds( 1 ), cancellationToken );
            }
        }
        catch (OperationCanceledException)
        {
            // Do nothing
        }
    }

    public ValueTask DisposeAsync()
    {
        // Nothing to dispose
        return ValueTask.CompletedTask;
    }
}