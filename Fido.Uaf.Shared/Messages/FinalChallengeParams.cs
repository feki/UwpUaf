using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class FinalChallengeParams
    {
        /// <summary>
        /// The value MUST be taken from the `AppId` field of the `OperationHeader`.
        /// </summary>
        /// <remarks>
        /// string[1..512]
        /// </remarks>
        [JsonProperty("appID", Required = Required.Always)]
        public string AppId { get; set; }

        /// <summary>
        /// The value MUST be taken from the challenge field of the request (e.g. `RegistrationRequest.challenge`, `AuthenticationRequest.challenge`).
        /// </summary>
        [JsonProperty("challenge", Required = Required.Always)]
        public string Challenge { get; set; }

        /// <summary>
        /// The value is determined by the FIDO UAF Client and it depends on the calling application.
        /// See [FIDOAppIDAndFacets](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-appid-and-facets-v1.0-ps-20141208.html)
        /// for more details. Security Relevance: The `FacetId` is determined by the FIDO UAF Client
        /// and verified against the list of trusted facets retrieved by dereferencing the `AppId` 
        /// of the calling application.
        /// </summary>
        /// <remarks>
        /// string[1..512]
        /// </remarks>
        [JsonProperty("facetID", Required = Required.Always)]
        public string FacetId { get; set; }

        /// <summary>
        /// Contains the TLS information to be sent by the FIDO Client to the FIDO Server,
        /// binding the TLS channel to the FIDO operation. 
        /// </summary>
        [JsonProperty("channelBinding", Required = Required.Always)]
        public ChannelBinding ChannelBinding { get; set; }
    }
}
