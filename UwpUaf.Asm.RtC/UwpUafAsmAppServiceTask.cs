using UwpUaf.Asm.Api;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;

namespace UwpUaf.Asm.RtC
{
    public sealed class UwpUafAsmAppServiceTask: IBackgroundTask
    {
        AppServiceConnection connection;
        BackgroundTaskDeferral serviceDeferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;

            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            connection = details.AppServiceConnection;

            //Listen for incoming app service requests
            connection.RequestReceived += OnRequestReceivedAsync;
        }

        void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (serviceDeferral != null)
            {
                //Complete the service deferral
                serviceDeferral.Complete();
                serviceDeferral = null;
            }
        }

        async static void OnRequestReceivedAsync(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            IAsmServiceRequestHandlers handlers = new AsmServiceRequestHandlers();
            var processor = new AsmServiceRequestProcessor(handlers);

            var deferral = args.GetDeferral();

            await processor.HandleAsmRequestAsync(args);

            deferral.Complete();
        }
    }
}
