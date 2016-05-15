using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationResponse: OperationResponseBase
    {
        /// <summary>
        /// The list of authenticator responses related to this operation.
        /// </summary>
        [JsonProperty("assertions", Required = Required.Always)]
        public AuthenticatorSignAssertion[] Assertions { get; set; }
    }
}
