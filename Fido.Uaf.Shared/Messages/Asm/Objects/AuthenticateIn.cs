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

        /// <summary>
        /// An array of transaction data to be confirmed by user. If multiple transactions are provided, then the ASM MUST select the one that best matches the current display characteristics.
        /// </summary>
        [JsonProperty("transaction")]
        public Transaction[] Transactions { get; set; }
    }
}
