using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Contains all fields related to the registration response.
    /// </summary>
    public class RegistrationResponse: OperationResponseBase
    {
        /// <summary>
        /// Response data for each Authenticator being registered.
        /// </summary>
        [JsonProperty("assertions", Required = Required.Always)]
        public AuthenticatorRegistrationAssertion[] Assertions { get; set; }
    }
}
