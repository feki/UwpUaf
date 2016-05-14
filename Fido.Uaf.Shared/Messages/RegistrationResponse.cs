namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Contains all fields related to the registration response.
    /// </summary>
    public class RegistrationResponse
    {
        /// <summary>
        /// Header.op MUST be "Reg".
        /// </summary>
        public OperationHeader Header { get; set; }

        /// <summary>
        /// The base64url-encoded serialized [RFC4627](https://tools.ietf.org/html/rfc4627)
        /// `FinalChallengeParams` using UTF8 encoding (see [FinalChallengeParams dictionary](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-protocol-v1.0-ps-20141208.html#finalchallenge-dictionary))
        /// which contains all parameters required for the server to verify the Final Challenge.
        /// </summary>
        public string FcParams { get; set; }

        /// <summary>
        /// Response data for each Authenticator being registered.
        /// </summary>
        public AuthenticatorRegistrationAssertion[] Assertions { get; set; }
    }
}
