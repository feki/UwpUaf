using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    public class AppRegistration
    {
        /// <summary>
        /// The FIDO server Application Identity.
        /// </summary>
        [JsonProperty("appID")]
        public string AppId { get; set; }

        /// <summary>
        /// List of key identifiers associated with the AppId.
        /// </summary>
        [JsonProperty("keyIDs")]
        public string[] KeyIds { get; set; }
    }
}
