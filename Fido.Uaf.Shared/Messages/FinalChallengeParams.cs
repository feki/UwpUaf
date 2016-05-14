namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class FinalChallengeParams
    {
        private string appId;
        private string challenge;
        private string facetId;
        private ChannelBinding channelBinding;

        /// <summary>
        /// The value MUST be taken from the `AppId` field of the `OperationHeader`.
        /// </summary>
        /// <remarks>
        /// string[1..512]
        /// </remarks>
        public string AppId
        {
            get { return appId; }
            set { appId = value; }
        }

        /// <summary>
        /// The value MUST be taken from the challenge field of the request (e.g. `RegistrationRequest.challenge`, `AuthenticationRequest.challenge`).
        /// </summary>
        public string Challenge
        {
            get { return challenge; }
            set { challenge = value; }
        }

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
        public string FacetId
        {
            get { return facetId; }
            set { facetId = value; }
        }

        /// <summary>
        /// Contains the TLS information to be sent by the FIDO Client to the FIDO Server,
        /// binding the TLS channel to the FIDO operation. 
        /// </summary>
        public ChannelBinding ChannelBinding
        {
            get { return channelBinding; }
            set { channelBinding = value; }
        }
    }
}
