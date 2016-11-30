using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fido.Uaf.Shared.Messages.Asm
{
    /// <summary>
    /// All ASM requests are represented as ASMRequest objects.
    /// </summary>
    public class AsmRequestBase
    {
        /// <summary>
        /// Request type.
        /// </summary>
        [JsonProperty("requestType", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Request RequestType { get; set; }

        /// <summary>
        /// ASM message version to be used with this request. For the definition of the Version
        /// dictionary see [UAFProtocol]. The ASM version MUST be 1.0 (i.e. major version is 1 and
        /// minor version 0).
        /// </summary>
        [JsonProperty("asmVersion")]
        public Version AsmVersion { get; set; } = new Version();

        /// <summary>
        /// Refer to the GetInfo request for more details. Field AuthenticatorIndex MUST NOT be set
        /// for GetInfo request.
        /// </summary>
        [JsonProperty("authenticatorIndex")]
        public ushort AuthenticatorIndex { get; set; }

        /// <summary>
        /// List of UAF extensions. For the definition of the Extension dictionary see [UAFProtocol].
        /// </summary>
        [JsonProperty("exts")]
        public Extension[] Exts { get; set; }
    }
}
