using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Newtonsoft.Json;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.System;

namespace UwpUaf.Asm.Api
{
    public class AsmApi : IAsmApi
    {
        public async Task<IReadOnlyList<AppInfo>> DiscoverAsmAsync()
        {
            return await Launcher.FindUriSchemeHandlersAsync(Constants.UwpUafAsmOperationProtocolScheme);
        }

        public async Task<IEnumerable<AsmGetInfo>> GetInfoAsync()
        {
            var ret = new List<AsmGetInfo>();
            var infos = await DiscoverAsmAsync();
            foreach (var info in infos)
            {
                var asmRes = await GetInfoAsync(info.PackageFamilyName);
                ret.Add(new AsmGetInfo { AppInfo = info, GetInfoOut = asmRes });
            }

            return ret;
        }

        public async Task<GetInfoOut> GetInfoAsync(string asmPackageFamilyName)
        {
            var asmRequest = new AsmRequestBase { RequestType = Request.GetInfo };
            var message = new ValueSet { { Constants.AsmMessageKey, JsonConvert.SerializeObject(asmRequest) } };

            AppServiceResponse response;
            using (var appService = new AppServiceConnection())
            {
                appService.AppServiceName = Constants.UwpUafAsmOperationProtocolScheme;
                appService.PackageFamilyName = asmPackageFamilyName;

                var openConnectionStatus = await appService.OpenAsync();
                if (openConnectionStatus != AppServiceConnectionStatus.Success)
                {
                    throw new UafAsmStatusException(StatusCode.UafAsmStatusError);
                }

                response = await appService.SendMessageAsync(message);
            }

            if (response.Status == AppServiceResponseStatus.Success)
            {
                var responseMessage = response.Message[Constants.AsmMessageKey] as string;
                var asmResponse = JsonConvert.DeserializeObject<AsmResponse<GetInfoOut>>(responseMessage);
                if (asmResponse.StatusCode != StatusCode.UafAsmStatusOk)
                {
                    throw new UafAsmStatusException(asmResponse.StatusCode);
                }

                return asmResponse.ResponseData;
            }
            else
            {
                throw new UafAsmStatusException(StatusCode.UafAsmStatusError);
            }
        }

        public async Task<RegisterOut> RegisterAsync(RegisterIn registerIn, string asmPackageFamilyName, ushort authenticatorIndex)
        {
            var result = await LaunchUriForResultsAsync<RegisterIn, RegisterOut>(Request.Register, registerIn, asmPackageFamilyName, authenticatorIndex);

            return result;
        }

        public async Task DeregisterAsync(DeregisterIn deregisterIn, string asmPackageFamilyName, ushort authenticatorIndex)
        {
            await LaunchUriForResultsAsync<DeregisterIn, object>(Request.Deregister, deregisterIn, asmPackageFamilyName, authenticatorIndex);
        }

        public async Task<AuthenticateOut> AuthenticateAsync(AuthenticateIn authenticateIn, string asmPackageFamilyName, ushort authenticatorIndex)
        {
            var result = await LaunchUriForResultsAsync<AuthenticateIn, AuthenticateOut>(Request.Authenticate, authenticateIn, asmPackageFamilyName, authenticatorIndex);

            return result;
        }

        async static Task<TOut> LaunchUriForResultsAsync<TIn, TOut>(Request requestType, TIn inData, string asmPackageFamilyName, ushort authenticatorIndex)
        {
            var asmRequest = new AsmRequest<TIn>
            {
                RequestType = requestType,
                AsmVersion = Fido.Uaf.Shared.Messages.Version.GetVersion_1_0(),
                AuthenticatorIndex = authenticatorIndex,
                Args = inData
            };
            var data = new ValueSet { { Constants.AsmMessageKey, JsonConvert.SerializeObject(asmRequest) } };

            var uri = new Uri($"{Constants.UwpUafAsmOperationProtocolScheme}:");
            var options = new LauncherOptions
            {
                TargetApplicationPackageFamilyName = asmPackageFamilyName
            };

            var result = await Launcher.LaunchUriForResultsAsync(uri, options, data);
            if (result.Status != LaunchUriStatus.Success || result.Result == null)
            {
                throw new UafAsmStatusException(StatusCode.UafAsmStatusError);
            }

            var responseMessage = result.Result[Constants.AsmMessageKey] as string;
            var asmResponse = JsonConvert.DeserializeObject<AsmResponse<TOut>>(responseMessage);
            if (asmResponse.StatusCode != StatusCode.UafAsmStatusOk)
            {
                throw new UafAsmStatusException(asmResponse.StatusCode);
            }

            return asmResponse.ResponseData;
        }
    }
}
