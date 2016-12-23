using System;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Newtonsoft.Json;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace UwpUaf.Asm.Api
{
    public class AsmServiceRequestProcessor
    {
        private readonly IAsmServiceRequestHandlers handlers;

        public AsmServiceRequestProcessor(IAsmServiceRequestHandlers handlers)
        {
            this.handlers = handlers;
        }

        public async Task HandleAsmRequestAsync(AppServiceRequestReceivedEventArgs args)
        {
            if (!args.Request.Message.ContainsKey(Constants.AsmMessageKey))
            {
                //TODO: Missing asm message data
            }

            var message = (string)args.Request.Message[Constants.AsmMessageKey];

            var asmRequest = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<AsmRequestBase>(message));
            AsmResponseBase asmResponse = null;
            if (asmRequest.RequestType == Request.GetInfo)
            {
                var request = JsonConvert.DeserializeObject<AsmRequestBase>(message);
                asmResponse = await handlers.ProcessGetInfoRequestAsync(request);
            }
            else
            {
                // TODO: Unsupported asm request type
                asmResponse = null;
            }

            var result = new ValueSet
            {
                { Constants.AsmMessageKey, JsonConvert.SerializeObject(asmResponse) }
            };

            var status = await args.Request.SendResponseAsync(result);
            //TODO: check AppServiceResponseStatus
            switch (status)
            {
                case AppServiceResponseStatus.Success:
                    break;
                case AppServiceResponseStatus.Failure:
                    break;
                case AppServiceResponseStatus.ResourceLimitsExceeded:
                    break;
                case AppServiceResponseStatus.Unknown:
                    break;
                case AppServiceResponseStatus.RemoteSystemUnavailable:
                    break;
                case AppServiceResponseStatus.MessageSizeTooLarge:
                    break;
            }
        }
    }
}
