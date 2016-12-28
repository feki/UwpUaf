using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;

namespace UwpUaf.Client.Api.Facet
{
    class FacetIdList
    {
        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("ids")]
        public string[] Ids { get; set; }
    }
}