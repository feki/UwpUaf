using Newtonsoft.Json;

namespace UwpUaf.Client.Api.Facet
{
    class FacetIdListResponse
    {
        [JsonProperty("trustedFacets")]
        public FacetIdList[] TrustedFacets { get; set; }
    }
}
