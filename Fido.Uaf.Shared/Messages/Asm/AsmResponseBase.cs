using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm
{
    /// <summary>
    /// All ASM responses are represented as ASMResponse objects.
    /// </summary>
    public class AsmResponseBase
    {
        /// <summary>
        /// MUST contain one of the values defined in the StatusCode interface.
        /// </summary>
        [JsonProperty("statusCode", Required = Required.Always)]
        public StatusCode StatusCode { get; set; }

        /// <summary>
        /// List of UAF extensions. For the definition of the Extension dictionary see [UAFProtocol].
        /// </summary>
        [JsonProperty("exts")]
        public Extension[] Exts { get; set; }
    }
}
