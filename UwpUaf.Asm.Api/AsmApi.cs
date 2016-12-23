using System;
using System.Collections.Generic;
using System.Linq;
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
                ret.Add(new AsmGetInfo { AppInfo = info, AsmResponse = asmRes });
            }

            return ret;
        }

        public async Task<AsmResponse<GetInfoOut>> GetInfoAsync(string asmPackageFamilyName)
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
                    // TODO: app service opening connection wasn't successful
                    return null;
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

                return asmResponse;
            }
            else
            {
                // TODO:
                throw new NotImplementedException();
            }
        }
    }
}
