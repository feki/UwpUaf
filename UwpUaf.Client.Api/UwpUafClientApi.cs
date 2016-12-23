using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Newtonsoft.Json;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.System;

namespace UwpUaf.Client.Api
{
    public class UwpUafClientApi
    {
        // TODO: Discover
        public async Task<DiscoveryData> Discover()
        {
            // It returns information about applications, that can handle UwpUaf.Client.Api protocol
            var appInfos = await Launcher.FindUriSchemeHandlersAsync(Constants.UwpUafClientOperationProtocolScheme);
            //var asmAppInfos = await asmApi.DiscoverAsm();
            //var asmGetInfos = await Task.WhenAll(asmAppInfos.Select(i => asmApi.GetInfo(i.PackageFamilyName)));
            //var authenticators = asmGetInfos.Select(i => i.ResponseData.Authenticators.AsEnumerable()).Aggregate((a, v) => a.Concat(v)).ToArray();

            //var discoveryData = new DiscoveryData
            //{
            //    SupportedUafVersions = new Fido.Uaf.Shared.Messages.Version[] { new Fido.Uaf.Shared.Messages.Version { Major = 1, Minor = 0, } },
            //    AvailableAuthenticators = authenticators,
            //    // TODO: must be set on initialization
            //    ClientVendor = "",
            //    ClientVersion = new Fido.Uaf.Shared.Messages.Version { Major = 1, Minor = 0, },
            //};

            var result = await MakeAppServiceRequestAsync(null, appInfos.First()?.PackageFamilyName);
            if (result?[Constants.UafIntentTypeKey] as string == Constants.UafIntentType.DiscoverResult)
            {

            }

            //return discoveryData;
            throw new NotImplementedException();
        }

        // TODO: CheckPolicy
        // TODO: Register
        public async Task Register(string sessionId, string appId, string challenge, string username, string asmPackageFamilyName, ChannelBinding channelBinding)
        {
            var registrationRequest = new RegistrationRequest
            {
                Header = new OperationHeader
                {
                    Upv = new Fido.Uaf.Shared.Messages.Version
                    {
                        Major = 1,
                        Minor = 0,
                    },
                    AppId = appId,
                    Op = Operation.Reg,
                    ServerData = sessionId,
                },
                Challenge = challenge,
                Username = username,
            };

            var request = new ValueSet
            {
                { Constants.UafIntentTypeKey, Constants.UafIntentType.UafOperation },
                { Constants.ClientMessageKey, JsonConvert.SerializeObject(new OperationRequestBase[] { registrationRequest }) },
                { Constants.ClientChannelBindingsKey, JsonConvert.SerializeObject(channelBinding) }
            };


            var result = await MakeLaunchUriForResultsRequestAsync(request, asmPackageFamilyName);
            if (result?[Constants.UafIntentTypeKey] as string == Constants.UafIntentType.UafOperationResult)
            {
                // TODO: implement
            }
            else
            {
                // TODO: UAF Operation Exception
                throw new NotImplementedException();
            }
        }
        // TODO: Authentication
        // TODO: Deregistration
        // TODO: UafOperationCompletionStatus

        private async Task<ValueSet> MakeAppServiceRequestAsync(ValueSet request, string clientPackageFamilyName)
        {
            AppServiceResponse response;
            using (var appService = new AppServiceConnection())
            {
                appService.AppServiceName = Constants.UwpUafClientOperationProtocolScheme;
                appService.PackageFamilyName = clientPackageFamilyName;
                var openConnectionStatus = await appService.OpenAsync();
                if (openConnectionStatus != AppServiceConnectionStatus.Success)
                {
                    // TODO: app service opening connection wasn't successful
                    throw new NotImplementedException();
                }

                response = await appService.SendMessageAsync(request);
            }

            if (response.Status == AppServiceResponseStatus.Success)
            {
                return response.Message;
            }
            else
            {
                // TODO:
                throw new NotImplementedException();
            }
        }

        private async Task<ValueSet> MakeLaunchUriForResultsRequestAsync(ValueSet request, string clientPackageFamilyName)
        {
            var uri = new Uri(Constants.UwpUafClientOperationProtocolScheme + ":");
            var options = new LauncherOptions
            {
                TargetApplicationPackageFamilyName = clientPackageFamilyName,
            };

            var result = await Launcher.LaunchUriForResultsAsync(uri, options, request);
            if (result.Status == LaunchUriStatus.Success)
            {
                return result.Result;
            }
            else
            {
                // TODO: implement exception
                throw new NotImplementedException();
            }
        }
    }
}
