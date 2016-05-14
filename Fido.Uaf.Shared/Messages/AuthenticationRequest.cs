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
        public OperationHeader Header { get; set; }

        /// <summary>
        /// Server-provided challenge value.
        /// </summary>
        public string Challenge { get; set; }

        /// <summary>
        /// Transaction data to be explicitly confirmed by the user.
        /// 
        /// The list contains the same transaction content in various content
        /// types and various image sizes.Refer to[UAFAuthnrMetadata] for more
        /// information about Transaction Confirmation Display characteristics.
        /// </summary>
        public Transaction[] Transaction { get; set; }

        /// <summary>
        /// Server-provided policy defining what types of authenticators
        /// are acceptable for this authentication operation.
        /// </summary>
        public Policy Policy { get; set; }
    }
}
