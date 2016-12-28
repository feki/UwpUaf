using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UwpUaf.Client.Api
{
    public class UafMessage
    {
        [JsonProperty("uafProtocolMessage")]
        public string UafProtocolMessage { get; set; }

        [JsonProperty("additionalData")]
        public object AdditionalData { get; set; }
    }
}
