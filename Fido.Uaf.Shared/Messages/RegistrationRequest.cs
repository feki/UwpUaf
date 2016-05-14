namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// RegistrationRequest contains a single, versioned, registration request.
    /// </summary>
    public class RegistrationRequest
    {
        /// <summary>
        /// It must be Operation.Reg
        /// </summary>
        public OperationHeader Header { get; set; }

        /// <summary>
        /// Server-provided challenge value.
        /// </summary>
        public string Challenge { get; set; }

        /// <summary>
        /// A human-readable user name intended to allow the user to distinguish
        /// and select from among different accounts at the same relying party.
        /// </summary>
        /// <remarks>
        /// string[1..128]
        /// </remarks>
        public string Username { get; set; }

        /// <summary>
        /// Describes which types of authenticators are acceptable for this registration operation.
        /// </summary>
        public Policy Policy { get; set; }
    }
}
