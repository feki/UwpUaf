using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    public abstract class RegAuthOperationRequestBase: OperationRequestBase
    {
        /// <summary>
        /// Server-provided challenge value.
        /// </summary>
        [JsonProperty("challenge", Required = Required.Always)]
        public string Challenge { get; set; }

        /// <summary>
        /// Describes which types of authenticators are acceptable for this registration/authentication operation.
        /// </summary>
        [JsonProperty("policy", Required = Required.Always)]
        public Policy Policy { get; set; }
    }
}
