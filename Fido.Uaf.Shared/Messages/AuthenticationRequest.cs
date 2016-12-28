using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class AuthenticationRequest: RegAuthOperationRequestBase
    {
        /// <summary>
        /// Transaction data to be explicitly confirmed by the user.
        ///
        /// The list contains the same transaction content in various content
        /// types and various image sizes.Refer to[UAFAuthnrMetadata] for more
        /// information about Transaction Confirmation Display characteristics.
        /// </summary>
        [JsonProperty("transaction", NullValueHandling = NullValueHandling.Ignore)]
        public Transaction[] Transactions { get; set; }

        public override bool ValidateMandatoryFields()
        {
            return base.ValidateMandatoryFields();
        }
    }
}
