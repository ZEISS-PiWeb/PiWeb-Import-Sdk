#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportAutomation;

namespace Zeiss.StartingAPlugin;

public sealed class ImportRunner(ICreateImportRunnerContext context) : IImportRunner
{
    private readonly IActivityService _statusService = context.ActivityService;

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            _statusService.SetActivity(
                new ActivityProperties()
                {
                    ActivityType = ActivityType.Normal,
                    ShortDisplayText = "Stage 1",
                    DetailedDisplayText = "Stage 1 - Some more details"
                },
                1);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            _statusService.ClearActivity();

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            _statusService.SetActivity(
                new ActivityProperties()
                {
                    ActivityType = ActivityType.Normal,
                    DetailedDisplayText = "Stage 2 - Waiting {0} seconds"
                },
                5);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            _statusService.SetActivity(
                new ActivityProperties()
                {
                    ActivityType = ActivityType.Suspension,
                    ShortDisplayText = "We triggered an error",
                    DetailedDisplayText = "We triggered an error",
                    IsSourceProblem = true
                });

            await Task.Delay(TimeSpan.FromMilliseconds(-1), cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // Do nothing
        }
    }
}
