using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Newtonsoft.Json;

namespace UwpUaf.Client.Api
{
    public class DiscoveryData
    {
        /// <summary>
        /// A list of the FIDO UAF protocol versions supported by the client, most-preferred first.
        /// </summary>
        [JsonProperty("supportedUAFVersions")]
        public Version[] SupportedUafVersions { get; set; }

        /// <summary>
        /// The vendor of the FIDO UAF Client.
        /// </summary>
        [JsonProperty("clientVendor")]
        public string ClientVendor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("clientVersion")]
        public Version ClientVersion { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("availableAuthenticators")]
        public AuthenticatorInfo[] AvailableAuthenticators { get; set; }
    }
}