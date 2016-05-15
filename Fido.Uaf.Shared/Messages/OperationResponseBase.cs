using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    public abstract class OperationResponseBase
    {
        /// <summary>
        /// Header.op
        ///  * MUST be Operation.Reg for RegistrationResponse.
        ///  * MUST be Operation.Auth for AuthenticationResponse.
        /// </summary>
        [JsonProperty("header", Required = Required.Always)]
        public OperationHeader Header { get; set; }

        /// <summary>
        /// The base64url-encoded serialized [RFC4627](https://tools.ietf.org/html/rfc4627)
        /// `FinalChallengeParams` using UTF8 encoding (see [FinalChallengeParams dictionary](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-protocol-v1.0-ps-20141208.html#finalchallenge-dictionary))
        /// which contains all parameters required for the server to verify the Final Challenge.
        /// </summary>
        [JsonProperty("fcParams", Required = Required.Always)]
        public string FcParams { get; set; }
    }
}
