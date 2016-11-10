using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    public class DeregisterIn
    {
        /// <summary>
        /// The FIDO server Application Identity.
        /// </summary>
        [JsonProperty("appID")]
        public string AppId { get; set; }

        /// <summary>
        /// Base64url-encoded [RFC4648] key identifier of the authenticator to be de-registered.
        /// </summary>
        [JsonProperty("keyID")]
        public string KeyId { get; set; }
    }
}
