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

            var asmRequest = JsonConvert.DeserializeObject<AsmRequestBase>(message);
            AsmResponseBase asmResponse = null;
            if (asmRequest.RequestType == Request.GetInfo)
            {
                asmResponse = await handlers.ProcessGetInfoRequestAsync(asmRequest);
            }
            else
            {
                // TODO: Unsupported asm request type
            }

            var result = new ValueSet
            {
                { Constants.AsmMessageKey, JsonConvert.SerializeObject(asmResponse) }
            };

            var status = await args.Request.SendResponseAsync(result);
        }
    }
}
