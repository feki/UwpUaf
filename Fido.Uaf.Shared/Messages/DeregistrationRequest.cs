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
        public OperationHeader Header { get; set; }

        /// <summary>
        /// List of authenticators to be deregistered.
        /// </summary>
        public DeregisterAuthenticator[] Authenticators { get; set; }
    }
}
