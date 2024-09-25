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
    private readonly IActivityService _activityService = context.ActivityService;

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        // This run method is called after an import plan is started. It runs on a thread pool thread in the background
        // and is not supposed to return until the cancellation is triggered via the given cancellation token.

        // The properties of the context variable can be used to interact with the host application. It also
        // provides the necessary location and authentication information to connect to the PiWeb backend configured by
        // the user in the import plan.

        // Usually we would do our custom data fetching and uploading in this method. However, since this is only a
        // demonstration plug-in, we do not do any data processing or upload at all here.

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            // The activity service can be used to alter the currently displayed activity in the hosting application.
            // We used this here to alter the current activity display after certain time intervals.

            _activityService.SetActivity(
                new ActivityProperties()
                {
                    ActivityType = ActivityType.Normal,
                    ShortDisplayText = "Stage 1",
                    DetailedDisplayText = "Stage 1 - Some more details"
                },
                1);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            _activityService.ClearActivity();

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            _activityService.SetActivity(
                new ActivityProperties()
                {
                    ActivityType = ActivityType.Normal,
                    DetailedDisplayText = "Stage 2 - Waiting {0} seconds"
                },
                5);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken).ConfigureAwait(false);

            // Suspension activities can be set to inform the user about errors that prevent this automation from
            // running as intended.

            _activityService.SetActivity(
                new ActivityProperties()
                {
                    ActivityType = ActivityType.Suspension,
                    ShortDisplayText = "We triggered an error",
                    DetailedDisplayText = "We triggered an error",
                    IsSourceProblem = true
                });

            // From here on we just wait indefinitely for the cancellation token to be triggered, and then we leave
            // the run method.

            await Task.Delay(TimeSpan.FromMilliseconds(-1), cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // Do nothing
        }
    }
}
