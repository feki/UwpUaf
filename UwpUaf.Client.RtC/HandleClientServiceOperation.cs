using System;
using System.Collections.Generic;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Newtonsoft.Json;
using UwpUaf.Asm.Api;
using UwpUaf.Client.Api;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;

namespace UwpUaf.Client.RtC
{
    public sealed class HandleClientServiceOperation : IBackgroundTask
    {
        private IAsmApi asmApi;
        private AppServiceConnection connection;
        private BackgroundTaskDeferral serviceDeferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;

            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            connection = details.AppServiceConnection;

            asmApi = new AsmApi();

            //Listen for incoming app service requests
            connection.RequestReceived += OnRequestReceivedAsync;
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (serviceDeferral != null)
            {
                //Complete the service deferral
                serviceDeferral.Complete();
                serviceDeferral = null;
            }
        }

        private async void OnRequestReceivedAsync(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            //Get a deferral so we can use an awaitable API to respond to the message
            var messageDeferral = args.GetDeferral();

            var req = args.Request.Message;
            if (!req.ContainsKey(Api.Constants.UafIntentTypeKey))
            {
                // TODO: corrupted message
            }
            else if (req[Api.Constants.UafIntentTypeKey] as string != Api.Constants.UafIntentType.Discover)
            {
                // TODO: unsupported UAF intent type
            }

            try
            {
                // 1. Zistit FamilyPackageName vsetkych ASM
                var infos = await asmApi.DiscoverAsmAsync();
                // 2. Zavolat GetInfo na kazdom z nich
                var authenticators = new List<AuthenticatorInfo>();
                foreach (var info in infos)
                {
                    var response = await asmApi.GetInfoAsync(info.PackageFamilyName);
                    authenticators.AddRange(response.ResponseData.Authenticators);
                }

                // 3. Vytvorit DiscoveryResult
                var discoveryData = new DiscoveryData
                {
                    ClientVendor = Shared.Constants.ClientVendor,
                    ClientVersion = Shared.Constants.ClientVersion,
                    SupportedUafVersions = new Fido.Uaf.Shared.Messages.Version[] { new Fido.Uaf.Shared.Messages.Version { Major = 1, Minor = 0 } },
                    AvailableAuthenticators = authenticators.ToArray()
                };

                //Create the response
                var result = new ValueSet
                {
                    { Api.Constants.UafIntentTypeKey, Api.Constants.UafIntentType.DiscoverResult },
                    { Api.Constants.ClientErrorCodeKey, (short)ErrorCode.NoError },
                    { Api.Constants.ClientDiscoveryDataKey, JsonConvert.SerializeObject(discoveryData) }
                };

                //Send the response
                await args.Request.SendResponseAsync(result);
            }
            finally
            {
                //Complete the message deferral so the platform knows we're done responding
                messageDeferral.Complete();
            }
        }
    }
}
