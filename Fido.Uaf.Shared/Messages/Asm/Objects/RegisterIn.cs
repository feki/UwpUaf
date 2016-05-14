namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterIn
    {
        /// <summary>
        /// The FIDO server Application Identity.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Human-readable user account name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// base64url-encoded challenge data [RFC4648].
        /// </summary>
        public string FinalChallenge { get; set; }

        /// <summary>
        /// Single requested attestation type.
        /// </summary>
        public short AttestationType { get; set; }
    }
}
