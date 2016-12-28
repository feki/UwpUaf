using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UwpUaf.Asm.Api;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.System;

namespace UwpUaf.Client.Api
{
    public class ClientApi : IClientApi
    {
        internal IReadOnlyDictionary<string, string> authenticatorIdToPackageFamilyName = new Dictionary<string, string>();
        DiscoveryData discoveryData;

        static ClientApi instance;
        readonly AsmApi asmApi;

        public static ClientApi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClientApi();
                }

                return instance;
            }
        }

        ClientApi()
        {
            asmApi = new AsmApi();
        }

        public async Task<DiscoveryData> DiscoverAsync()
        {
            if (discoveryData == null)
            {
                AppServiceResponse response;
                var message = new ValueSet
                {
                    { Constants.UafIntentTypeKey, Constants.UafIntentType.Discover }
                };

                using (var appService = new AppServiceConnection())
                {
                    appService.AppServiceName = Constants.UwpUafClientOperationProtocolScheme;
                    appService.PackageFamilyName = ClientApiSettings.UwpUafClientPackageFamilyName;
                    var openConnectionStatus = await appService.OpenAsync();
                    if (openConnectionStatus != AppServiceConnectionStatus.Success)
                    {
                        throw new FidoOperationErrorCodeException(ErrorCode.Unknown);
                    }

                    response = await appService.SendMessageAsync(message);
                }

                if (response.Status == AppServiceResponseStatus.Success)
                {
                    var res = response.Message;

                    CheckResponseIntentType(res);
                    CheckResponseErrorCode(res);
                    discoveryData = CheckAndGetResponseDiscoveryData(res);

                    StoreAuthenticatorIdToPackageFamilyNameDictionary(res);
                }
            }

            return discoveryData;
        }

        void StoreAuthenticatorIdToPackageFamilyNameDictionary(ValueSet res)
        {
            var dicJson = res[Constants.AuthenticatorIdToPackageFamilyNameDictionaryKey] as string;
            authenticatorIdToPackageFamilyName = JsonConvert.DeserializeObject<Dictionary<string, string>>(dicJson);
        }

        static DiscoveryData CheckAndGetResponseDiscoveryData(ValueSet res)
        {
            if (!res.ContainsKey(Constants.ClientDiscoveryDataKey))
            {
                throw new FidoOperationErrorCodeException(ErrorCode.Unknown);
            }

            return JsonConvert.DeserializeObject<DiscoveryData>(res[Constants.ClientDiscoveryDataKey] as string);
        }

        static void CheckResponseErrorCode(ValueSet res)
        {
            if (!res.ContainsKey(Constants.ClientErrorCodeKey))
            {
                throw new FidoOperationErrorCodeException(ErrorCode.Unknown);
            }

            var errorCode = (ErrorCode)Convert.ToInt16(res[Constants.ClientErrorCodeKey]);
            if (errorCode != ErrorCode.NoError)
            {
                throw new FidoOperationErrorCodeException(errorCode);
            }
        }

        static void CheckResponseIntentType(ValueSet res)
        {
            if (!res.ContainsKey(Constants.UafIntentTypeKey))
            {
                throw new FidoOperationErrorCodeException(ErrorCode.Unknown);
            }
            else if (res[Constants.UafIntentTypeKey] as string != Constants.UafIntentType.DiscoverResult)
            {
                throw new FidoOperationErrorCodeException(ErrorCode.Unknown);
            }
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest registrationRequest, ChannelBinding channelBinding)
        {
            var message = new UafMessage
            {
                UafProtocolMessage = JsonConvert.SerializeObject(new OperationRequestBase[] { registrationRequest })
            };

            var result = await SendOperationAsync(message, channelBinding);
            var registrationResponeJson = result.UafProtocolMessage;

            var response = UafMessageUtils.GetUafV10Message(result.UafProtocolMessage);

            return response.ToObject<RegistrationResponse>();
        }

        async static Task<UafMessage> SendOperationAsync(UafMessage message, ChannelBinding channelBinding)
        {
            var data = new ValueSet
            {
                { Constants.UafIntentTypeKey, Constants.UafIntentType.UafOperation },
                { Constants.ClientMessageKey, JsonConvert.SerializeObject(message) },
                { Constants.ClientChannelBindingsKey, JsonConvert.SerializeObject(channelBinding) }
            };

            var uri = new Uri($"{Constants.UwpUafClientOperationProtocolScheme}:");
            var options = new LauncherOptions
            {
                TargetApplicationPackageFamilyName = ClientApiSettings.UwpUafClientPackageFamilyName
            };

            var result = await Launcher.LaunchUriForResultsAsync(uri, options, data);
            if (result.Status != LaunchUriStatus.Success)
            {
                // TODO:
            }

            var retUafIntentType = result.Result[Constants.UafIntentTypeKey] as string;
            if (retUafIntentType != Constants.UafIntentType.UafOperationResult)
            {
                // TODO:
            }

            CheckResponseErrorCode(result.Result);

            var ret = result.Result[Constants.ClientMessageKey] as string;

            return JsonConvert.DeserializeObject<UafMessage>(ret);
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest, ChannelBinding channelBinding)
        {
            var message = new UafMessage
            {
                UafProtocolMessage = JsonConvert.SerializeObject(new OperationRequestBase[] { authenticationRequest })
            };

            var result = await SendOperationAsync(message, channelBinding);
            var authenticationResponeJson = result.UafProtocolMessage;

            return JsonConvert.DeserializeObject<AuthenticationResponse>(authenticationResponeJson);
        }

        public async Task DeregisterAsync(DeregistrationRequest deregistrationRequest, ChannelBinding channelBinding)
        {
            var message = new UafMessage
            {
                UafProtocolMessage = JsonConvert.SerializeObject(new OperationRequestBase[] { deregistrationRequest })
            };

            var result = await SendOperationAsync(message, channelBinding);
        }

        public async Task<IList<AuthenticatorInfo>> GetAvailableAuthenticatorsAsync()
        {
            var authenticatorIdToPackageFamilyNameDictionary = new Dictionary<string, string>();

            // 1. Zistit FamilyPackageName vsetkych ASM
            var infos = await asmApi.DiscoverAsmAsync();
            // 2. Zavolat GetInfo na kazdom z nich
            var authenticators = new List<AuthenticatorInfo>();
            foreach (var info in infos)
            {
                var getInfoOut = await asmApi.GetInfoAsync(info.PackageFamilyName);
                authenticators.AddRange(getInfoOut.Authenticators);
                foreach (var auth in getInfoOut.Authenticators)
                {
                    authenticatorIdToPackageFamilyNameDictionary.Add(auth.Aaid, info.PackageFamilyName);
                }
            }

            authenticatorIdToPackageFamilyName = authenticatorIdToPackageFamilyNameDictionary;

            return authenticators;
        }
    }
}
