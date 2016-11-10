using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    public class AuthenticateIn
    {
        /// <summary>
        /// The FIDO server Application Identity.
        /// </summary>
        [JsonProperty("appID")]
        public string AppId { get; set; }

        /// <summary>
        /// base64url [RFC4648] encoded keyIDs.
        /// </summary>
        [JsonProperty("keyIDs")]
        public string[] KeyIds { get; set; }

        /// <summary>
        /// base64url [RFC4648] encoded final challenge.
        /// </summary>
        [JsonProperty("finalChallenge")]
        public string FinalChallenge { get; set; }

        //[JsonProperty("transaction")]
        //public Transaction[] Transactions { get; set; }
    }
}
