using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    public abstract class OperationRequestBase
    {
        /// <summary>
        /// Header.Op
        ///  * MUST be Operation.Reg for RegistrationRequest.
        ///  * MUST be Operation.Auth for AuthenticationRequest.
        ///  * MUST be Operation.Dereg for DeregistrationRequest.
        /// </summary>
        [JsonProperty("header", Required = Required.Always)]
        public OperationHeader Header { get; set; }
    }
}
