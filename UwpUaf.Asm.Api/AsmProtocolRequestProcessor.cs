﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Collections;

namespace UwpUaf.Asm.Api
{
    /// <summary>
    ///
    /// </summary>
    public class AsmProtocolRequestProcessor
    {
        readonly IAsmProtocolRequestHandlers handlers;

        IDictionary<Request, Type> AsmRequestTypeToObjectTypeMap { get; } = new Dictionary<Request, Type>
        {
            { Request.Register, typeof(AsmRequest<RegisterIn>) },
            { Request.Authenticate, typeof(AsmRequest<AuthenticateIn>) },
            { Request.Deregister, typeof(AsmRequest<DeregisterIn>) }
        };

        IDictionary<Request, Func<AsmRequestBase, IAsmProtocolRequestHandlers, Task<AsmResponseBase>>> AsmRequestTypeToRequestProcessorMap { get; } =
            new Dictionary<Request, Func<AsmRequestBase, IAsmProtocolRequestHandlers, Task<AsmResponseBase>>>
            {
                { Request.Register, async (request, handlers) => await handlers.ProcessRegisterRequestAsync(request as AsmRequest<RegisterIn>) },
                { Request.Authenticate, async (request, handlers) => await handlers.ProcessAuthenticateRequestAsync(request as AsmRequest<AuthenticateIn>) },
                { Request.Deregister, async (request, handlers) => await handlers.ProcessDeregisterRequestAsync(request as AsmRequest<DeregisterIn>) }
            };

        public AsmProtocolRequestProcessor(IAsmProtocolRequestHandlers handlers)
        {
            this.handlers = handlers;
        }

        /// <summary>
        /// It handles application's activation protocol for results, for UwfUaf protocol.
        /// It must be launch with additional data:
        /// var data = new ValueSet
        /// {
        ///     { "message", "&lt;ASMRequest message encoded to Base64 string in UTF-8 encoding&gt;" }
        /// }
        /// </summary>
        /// <param name="activatedEventArgs"></param>
        public async Task HandleAsmRequestAsync(IActivatedEventArgs activatedEventArgs)
        {
            if (activatedEventArgs.Kind != ActivationKind.ProtocolForResults)
            {
                //TODO: Unsupported application activation
                return;
            }

            var args = activatedEventArgs as ProtocolForResultsActivatedEventArgs;
            if (!args.Uri.Scheme.Equals(Constants.UwpUafAsmOperationProtocolScheme, StringComparison.OrdinalIgnoreCase))
            {
                //TODO: Unsupported protocol scheme
                return;
            }

            if (!args.Data.ContainsKey(Constants.AsmMessageKey))
            {
                //TODO: Missing asm message data
                return;
            }

            var message = (string)args.Data[Constants.AsmMessageKey];
            // Deserialize ams request from message json string
            var asmRequest = DeserializeAsmRequestJson(message);
            // Process asm request
            var asmResponse = await ProcessAsmRequestAsync(asmRequest);


            var result = new ValueSet
            {
                { Constants.AsmMessageKey, JsonConvert.SerializeObject(asmResponse) }
            };
            // Report result to calling application
            args.ProtocolForResultsOperation.ReportCompleted(result);
        }

        AsmRequestBase DeserializeAsmRequestJson(string asmMessageJson)
        {
            JObject messageObject;
            try
            {
                // Parse message json string as JObject
                messageObject = JObject.Parse(asmMessageJson);
            }
            catch (JsonReaderException)
            {
                //TODO: malformed message json string
                throw;
            }

            Request asmRequestType;
            try
            {
                var stringEnumConverter = new StringEnumConverter();
                var jsonSerializer = new JsonSerializer();
                jsonSerializer.Converters.Add(stringEnumConverter);

                asmRequestType = messageObject.SelectToken("requestType", true).ToObject<Request>(jsonSerializer);
            }
            catch (Exception)
            {
                //TODO: non-existing or malformed requestType property
                throw;
            }

            if (!AsmRequestTypeToObjectTypeMap.ContainsKey(asmRequestType))
            {
                //TODO: unsupported asm request type
            }

            var asmType = AsmRequestTypeToObjectTypeMap[asmRequestType];
            return (AsmRequestBase)messageObject.ToObject(asmType);
        }

        async Task<AsmResponseBase> ProcessAsmRequestAsync(AsmRequestBase asmRequest)
        {
            var processor = AsmRequestTypeToRequestProcessorMap[asmRequest.RequestType];
            return await processor(asmRequest, handlers);
        }
    }
}
