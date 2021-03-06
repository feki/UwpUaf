﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Represents a UAF message Request and Response header
    /// </summary>
    public class OperationHeader
    {
        /// <summary>
        /// UAF protocol version.
        /// </summary>
        /// <note>
        /// For FIDO 1.0 specification:
        /// Major version MUST be `1` and minor version MUST be `0`.
        /// </note>
        [JsonProperty("upv", Required = Required.Always)]
        public Version Upv { get; set; }

        /// <summary>
        /// Name of FIDO operation this message relates to.
        /// </summary>
        /// <note>
        /// Auth is used for both authentication and transaction confirmation.
        /// </note>
        [JsonProperty("op", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Operation Op { get; set; }

        /// <summary>
        /// The application identifier that the relying party would like to assert.
        /// </summary>
        /// <remarks>
        /// `string[0..512]`
        /// </remarks>
        /// <note>
        /// There are three ways to set the `AppId` [FIDOAppIDAndFacets](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-appid-and-facets-v1.0-ps-20141208.html):
        ///
        ///  * If the element is missing or empty in the request, the FIDO UAF Client MUST set it to the `FacetId` of the caller.
        ///  * If the `AppId` present in the message is identical to the `FacetId` of the caller, the FIDO UAF Client MUST accept it.
        ///  * If it is an URI with HTTPS protocol scheme, the FIDO UAF Client MUST use it to load the list of trusted facet identifiers from the specified URI.The FIDO UAF Client MUST only accept the request, if the facet identifier of the caller matches one of the trusted facet identifiers in the list returned from dereferencing this URI.
        ///
        ///
        /// The new key pair that the authenticator generates will be associated with this application identifier.
        ///
        /// Security Relevance: The application identifier is used by the FIDO UAF Client to verify the eligibility of an application to trigger the use of a specific `UAuth.Key`. See[FIDOAppIDAndFacets](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-protocol-v1.0-ps-20141208.html#bib-FIDOAppIDAndFacets)
        /// </note>
        [JsonProperty("appID", NullValueHandling = NullValueHandling.Ignore)]
        public string AppId { get; set; }

        /// <summary>
        /// A session identifier created by the relying party.
        /// </summary>
        /// <remarks>
        /// string[1..1536]
        /// </remarks>
        /// <note>
        /// The relying party can opaquely store things like expiration times for the registration session, protocol version used and other useful information in `serverData`. This data is opaque to FIDO UAF Clients. FIDO Servers may reject a response that is lacking this data or is containing unauthorized modifications to it.
        ///
        /// Servers that depend on the integrity of `serverData` should apply appropriate security measures, as described in [Registration Request Generation Rules for FIDO Server](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-protocol-v1.0-ps-20141208.html#registration-request-generation-rules-for-fido-server) and section [ServerData and KeyHandle](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-protocol-v1.0-ps-20141208.html#serverdata-and-keyhandle).
        /// </note>
        [JsonProperty("serverData", NullValueHandling = NullValueHandling.Ignore)]
        public string ServerData { get; set; }

        /// <summary>
        /// List of UAF Message Extensions.
        /// </summary>
        [JsonProperty("exts", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Extension> Exts { get; set; }
    }
}
