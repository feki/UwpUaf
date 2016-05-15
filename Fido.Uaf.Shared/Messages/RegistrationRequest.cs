using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// RegistrationRequest contains a single, versioned, registration request.
    /// </summary>
    public class RegistrationRequest: RegAuthOperationRequestBase
    {
        /// <summary>
        /// A human-readable user name intended to allow the user to distinguish
        /// and select from among different accounts at the same relying party.
        /// </summary>
        /// <remarks>
        /// string[1..128]
        /// </remarks>
        [JsonProperty("username", Required = Required.Always)]
        public string Username { get; set; }
    }
}
