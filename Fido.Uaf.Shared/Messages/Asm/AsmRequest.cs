using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fido.Uaf.Shared.Messages.Asm
{
    /// <summary>
    /// All ASM requests are represented as ASMRequest objects.
    /// </summary>
    public class AsmRequest<T>: AsmRequestBase
    {
        /// <summary>
        /// Request-specific arguments. If set, this attribute MAY take one of the following types:
        /// 
        /// * RegisterIn
        /// * AuthenticateIn
        /// * DeregisterIn
        /// </summary>
        [JsonProperty("args")]
        public T Args { get; set; }
    }
}
