using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class DeregistrationRequest: OperationRequestBase
    {
        /// <summary>
        /// List of authenticators to be deregistered.
        /// </summary>
        [JsonProperty("authenticators", Required = Required.Always)]
        public DeregisterAuthenticator[] Authenticators { get; set; }

        public override bool ValidateMandatoryFields()
        {
            return base.ValidateMandatoryFields() && Authenticators != null;
        }
    }
}
