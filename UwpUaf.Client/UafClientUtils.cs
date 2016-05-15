using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UwpUaf.Client
{
    public class UafClientUtils
    {
        /// <summary>
        /// It gets UAF message of UAF Protocol version 1.0.
        /// </summary>
        /// <param name="uafMessage">JSON array of UAF messages</param>
        /// <returns>UAF message of protocol version 1.0</returns>
        public JObject GetUafV10Message(string uafMessage)
        {
            var messages = JArray.Parse(uafMessage);
            return messages.First((item) =>
            {
                var header = JsonConvert.DeserializeObject<Fido.Uaf.Shared.Messages.Version>(item["header"]["upv"].ToString());
                return header.Major == 1 && header.Minor == 0;
            }) as JObject;
        }

        /// <summary>
        /// It returns UAF message operation type.
        /// </summary>
        /// <param name="message">UAF message of protocol version 1.0</param>
        /// <returns>operation of UAF message</returns>
        public Operation GetMessageOperation(JObject message)
        {
            return JsonConvert.DeserializeObject<OperationHeader>(message["header"].ToString()).Op;
        }

        /// <summary>
        /// It processes UAF message of protocol version 1.0.
        /// </summary>
        /// <param name="message">UAF message of protocol version 1.0</param>
        public void ProcessMessageV10(JObject message)
        {
            
        }
    }
}
