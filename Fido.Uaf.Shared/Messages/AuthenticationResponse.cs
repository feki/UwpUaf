namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationResponse
    {
        /// <summary>
        /// Header.Op MUST be Operation.Auth.
        /// </summary>
        public OperationHeader Header { get; set; }

        /// <summary>
        /// The field fcParams is the base64url-encoded serialized [RFC4627](https://tools.ietf.org/html/rfc4627)
        /// FinalChallengeParams in UTF8 encoding (see [FinalChallengeParams dictionary](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-protocol-v1.0-ps-20141208.html#finalchallenge-dictionary))
        /// which contains all parameters required for the server to verify the Final Challenge.
        /// </summary>
        public string FcParams { get; set; }

        /// <summary>
        /// The list of authenticator responses related to this operation.
        /// </summary>
        public AuthenticatorSignAssertion[] Assertions { get; set; }
    }
}
