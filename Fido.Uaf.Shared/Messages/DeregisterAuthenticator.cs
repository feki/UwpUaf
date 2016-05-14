namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Deregister authenticator.
    /// </summary>
    public class DeregisterAuthenticator
    {
        /// <summary>
        /// AAID of the authenticator to deregister.
        /// </summary>
        public string Aaid { get; set; }

        /// <summary>
        /// The unique KeyID related to `UAuth.priv`.
        /// KeyID is assumed to be unique within the scope of an AAID only.
        /// </summary>
        public string KeyId { get; set; }
    }
}
