using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class GetInfoOut
    {
        /// <summary>
        /// List of authenticators reported by the current ASM. MAY be empty an empty list.
        /// </summary>
        [JsonProperty("Authenticators")]
        public AuthenticatorInfo[] Authenticators { get; set; }
    }
}
