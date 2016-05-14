namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Describes the operation type of a UAF message or request for a message.
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// Registration
        /// </summary>
        Reg,

        /// <summary>
        /// Authentication or Transaction Confirmation
        /// </summary>
        Auth,

        /// <summary>
        /// Deregistration
        /// </summary>
        Dereg
    }
}
