using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UwpUaf.Client.Api.Operations
{
    public static class UafMessageUtils
    {
        private static readonly IDictionary<Operation, Type> opToRequestType = new Dictionary<Operation, Type>
        {
            { Operation.Reg, typeof(RegistrationRequest) },
            { Operation.Auth, typeof(AuthenticationRequest) },
            { Operation.Dereg, typeof(DeregistrationRequest) }
        };

        /// <summary>
        /// It gets UAF message of UAF Protocol version 1.0.
        /// </summary>
        /// <param name="uafMessage">JSON array of UAF messages</param>
        /// <returns>UAF message of protocol version 1.0</returns>
        public static JObject GetUafV10Message(string uafMessage)
        {
            var uaf = JsonConvert.DeserializeObject<UafMessage>(uafMessage);
            var messages = JArray.Parse(uaf.UafProtocolMessage);

            return messages.First((item) =>
            {
                var header = GetMessageOperationHeader(item as JObject);

                return header.Upv.Major == 1 && header.Upv.Minor == 0;
            }) as JObject;
        }

        public static OperationHeader GetMessageOperationHeader(JObject message)
        {
            return message.SelectToken("header").ToObject<OperationHeader>();
        }

        public static OperationRequestBase ParseMessage(JObject message)
        {
            var op = GetMessageOperationHeader(message).Op;

            return (OperationRequestBase)message.ToObject(opToRequestType[op]);
        }
    }
}
