using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    /// <summary>
    ///
    /// </summary>
    public class RegisterIn
    {
        /// <summary>
        /// The FIDO server Application Identity.
        /// </summary>
        [JsonProperty("appID")]
        public string AppId { get; set; }

        /// <summary>
        /// Human-readable user account name.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// base64url-encoded challenge data [RFC4648].
        /// </summary>
        [JsonProperty("finalChallenge")]
        public string FinalChallenge { get; set; }

        /// <summary>
        /// Single requested attestation type.
        /// </summary>
        [JsonProperty("attestationType")]
        public short AttestationType { get; set; }
    }
}
