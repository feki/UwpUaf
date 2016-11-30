using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm
{
    /// <summary>
    /// All ASM responses are represented as ASMResponse objects.
    /// </summary>
    public class AsmResponse<T>: AsmResponseBase
    {
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
    }
}
