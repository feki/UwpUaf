using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm
{
    /// <summary>
    /// All ASM responses are represented as ASMResponse objects.
    /// </summary>
    public class AsmResponse<T>
    {
        /// <summary>
        /// MUST contain one of the values defined in the StatusCode interface.
        /// </summary>
        [JsonProperty("statusCode", Required = Required.Always)]
        public StatusCode StatusCode { get; set; }

        /// <summary>
        /// Request-specific response data. This attribute MUST have one of the following types:
        /// 
        /// GetInfoOut
        /// RegisterOut
        /// AuthenticateOut
        /// GetRegistrationOut
        /// </summary>
        [JsonProperty("responseData")]
        public T ResponseData { get; set; }

        /// <summary>
        /// List of UAF extensions. For the definition of the Extension dictionary see [UAFProtocol].
        /// </summary>
        [JsonProperty("exts")]
        public Extension[] Exts { get; set; }
    }
}
