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
        readonly IAsmServiceRequestHandlers handlers;

        public AsmServiceRequestProcessor(IAsmServiceRequestHandlers handlers)
        {
            this.handlers = handlers;
        }

        public async Task HandleAsmRequestAsync(AppServiceRequestReceivedEventArgs args)
        {
            AsmResponseBase asmResponse = null;
            if (args.Request.Message.ContainsKey(Constants.AsmMessageKey))
            {
                try
                {
                    var message = (string)args.Request.Message[Constants.AsmMessageKey];

                    var asmRequest = JsonConvert.DeserializeObject<AsmRequestBase>(message);
                    asmResponse = asmRequest.RequestType == Request.GetInfo ? (AsmResponseBase)await handlers.ProcessGetInfoRequestAsync(asmRequest) : CreateErrorAsmResponse();
                }
                catch (Exception)
                {
                    asmResponse = CreateErrorAsmResponse();
                }
            }
            else
            {
                asmResponse = CreateErrorAsmResponse();
            }

            var result = new ValueSet
            {
                { Constants.AsmMessageKey, JsonConvert.SerializeObject(asmResponse) }
            };

            var status = await args.Request.SendResponseAsync(result);
        }

        static AsmResponseBase CreateErrorAsmResponse()
        {
            return new AsmResponseBase
            {
                StatusCode = StatusCode.UafAsmStatusError
            };
        }
    }
}
