using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UwpUaf.Client.Api.Facet
{
    class FacetIdListResponse
    {
        [JsonProperty("trustedFacets")]
        public FacetIdList[] TrustedFacets { get; set; }
    }
}
