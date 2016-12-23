using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace UwpUaf.Client.Api
{
    public class ClientApi : IClientApi
    {
        public async Task<DiscoveryData> DiscoverAsync()
        {
            AppServiceResponse response;
            using (var appService = new AppServiceConnection())
            {
                appService.AppServiceName = Constants.UwpUafClientOperationProtocolScheme;
                appService.PackageFamilyName = ClientApiSettings.UwpUafClientPackageFamilyName;
                var openConnectionStatus = await appService.OpenAsync();
                if (openConnectionStatus != AppServiceConnectionStatus.Success)
                {
                    // TODO: app service opening connection wasn't successful
                }

                var message = new ValueSet
                {
                    { Constants.UafIntentTypeKey, Constants.UafIntentType.Discover }
                };
                response = await appService.SendMessageAsync(message);
            }

            if (response.Status == AppServiceResponseStatus.Success)
            {
                var res = response.Message;

                CheckResponseIntentType(res);
                CheckResponseErrorCode(res);
                var discoveryData = CheckAndGetResponseDiscoveryData(res);

                return discoveryData;
            }
            else
            {
                // TODO:
                throw new NotImplementedException();
            }
        }

        private DiscoveryData CheckAndGetResponseDiscoveryData(ValueSet res)
        {
            if (!res.ContainsKey(Constants.ClientDiscoveryDataKey))
            {
                // TODO:
            }

            return JsonConvert.DeserializeObject<DiscoveryData>(res[Constants.ClientDiscoveryDataKey] as string);
        }

        private void CheckResponseErrorCode(ValueSet res)
        {
            if (!res.ContainsKey(Constants.ClientErrorCodeKey))
            {
                // TODO:
            }

            var errorCode = (ErrorCode)Convert.ToInt16(res[Constants.ClientErrorCodeKey]);
            if (errorCode != ErrorCode.NoError)
            {
                throw new FidoOperationErrorCodeException(errorCode);
            }
        }

        private void CheckResponseIntentType(ValueSet res)
        {
            if (!res.ContainsKey(Constants.UafIntentTypeKey))
            {
                // TODO:
            }
            else if (res[Constants.UafIntentTypeKey] as string != Constants.UafIntentType.DiscoverResult)
            {
                // TODO:
            }
        }
    }
}
