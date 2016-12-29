using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    public class GetRegistrationsOut
    {
        /// <summary>
        /// List of registrations associated with an AppId. MAY be an empty list.
        /// </summary>
        [JsonProperty("appRegs")]
        public AppRegistration[] AppRegs { get; set; }
    }
}
