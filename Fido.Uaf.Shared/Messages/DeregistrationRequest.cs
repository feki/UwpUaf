using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class DeregistrationRequest
    {
        /// <summary>
        /// Header.Op MUST be Operation.Dereg.
        /// </summary>
        [JsonProperty("header", Required = Required.Always)]
        public OperationHeader Header { get; set; }

        /// <summary>
        /// List of authenticators to be deregistered.
        /// </summary>
        [JsonProperty("authenticators", Required = Required.Always)]
        public DeregisterAuthenticator[] Authenticators { get; set; }
    }
}
