using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// Header.Op MUST be Operation.Auth.
        /// </summary>
        [JsonProperty("header", Required = Required.Always)]
        public OperationHeader Header { get; set; }

        /// <summary>
        /// Server-provided challenge value.
        /// </summary>
        [JsonProperty("challenge", Required = Required.Always)]
        public string Challenge { get; set; }

        /// <summary>
        /// Transaction data to be explicitly confirmed by the user.
        /// 
        /// The list contains the same transaction content in various content
        /// types and various image sizes.Refer to[UAFAuthnrMetadata] for more
        /// information about Transaction Confirmation Display characteristics.
        /// </summary>
        [JsonProperty("transaction")]
        public Transaction[] Transactions { get; set; }

        /// <summary>
        /// Server-provided policy defining what types of authenticators
        /// are acceptable for this authentication operation.
        /// </summary>
        [JsonProperty("policy")]
        public Policy Policy { get; set; }
    }
}
