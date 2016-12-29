using System;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Collections;
using Windows.System;
using UwpUaf.Client.Api.Operations;

namespace UwpUaf.Client.Api
{
    public class ClientProtocolMessageProcessor
    {
        readonly IClientProtocolOperationHandlers handlers;

        public ClientProtocolMessageProcessor(IClientProtocolOperationHandlers handlers)
        {
            this.handlers = handlers;
        }

        public async Task HandleUafMessageAsync(IActivatedEventArgs activatedEventArgs)
        {
            if (activatedEventArgs.Kind != ActivationKind.ProtocolForResults)
            {
                return;
            }

            var args = activatedEventArgs as ProtocolForResultsActivatedEventArgs;
            if (!args.Uri.Scheme.Equals(Constants.UwpUafClientOperationProtocolScheme, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (!args.Data.ContainsKey(Constants.ClientMessageKey))
            {
                return;
            }

            var result = new ValueSet();
            var data = args.Data;

            var uafMessageJson = data[Constants.ClientMessageKey] as string;
            var channelBindingJson = data[Constants.ClientChannelBindingsKey] as string;
            var channelBinding = JsonConvert.DeserializeObject<ChannelBinding>(channelBindingJson);

            try
            {
                var op = OperationBase.GetOperationFromUafMessage(uafMessageJson);
                op.CallerPackageFamilyName = args.CallerPackageFamilyName;
                op.ChannelBinding = channelBinding;
                op.Handlers = handlers;
                if (op.CheckMandatoryFields())
                {
                    var opResult = await op.ProcessOperationAsync();
                    result.Add(Constants.UafIntentTypeKey, Constants.UafIntentType.UafOperationResult);
                    result.Add(Constants.ClientErrorCodeKey, (short)ErrorCode.NoError);
                    var protocolMessage = new OperationResponseBase[] { opResult };
                    var uafMessage = new UafMessage
                    {
                        UafProtocolMessage = JsonConvert.SerializeObject(protocolMessage)
                    };

                    result.Add(Constants.ClientMessageKey, JsonConvert.SerializeObject(uafMessage));
                }
                else
                {
                    throw new FidoOperationErrorCodeException(ErrorCode.ProtocolError);
                }
            }
            catch (FidoOperationErrorCodeException e)
            {
                ReportErrorResult(args.ProtocolForResultsOperation, e.ErrorCode);
            }

            args.ProtocolForResultsOperation.ReportCompleted(result);
        }

        static void ReportErrorResult(ProtocolForResultsOperation operation, ErrorCode errorCode)
        {
            var resultData = new ValueSet
            {
                { Constants.UafIntentTypeKey, Constants.UafIntentType.UafOperationResult },
                { Constants.ClientErrorCodeKey, (short)errorCode }
            };

            operation.ReportCompleted(resultData);
        }
    }
}
