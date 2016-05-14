namespace Fido.Uaf.Shared.Messages.Asm
{
    /// <summary>
    /// All ASM requests are represented as ASMRequest objects.
    /// </summary>
    public class AsmRequest<T>
    {
        /// <summary>
        /// Request type.
        /// </summary>
        public Request RequestType { get; set; }

        /// <summary>
        /// ASM message version to be used with this request. For the definition of the Version
        /// dictionary see [UAFProtocol]. The ASM version MUST be 1.0 (i.e. major version is 1 and
        /// minor version 0).
        /// </summary>
        public Version AsmVersion { get; set; } = new Version();

        /// <summary>
        /// Refer to the GetInfo request for more details. Field AuthenticatorIndex MUST NOT be set
        /// for GetInfo request.
        /// </summary>
        public short AuthenticatorIndex { get; set; }

        /// <summary>
        /// Request-specific arguments. If set, this attribute MAY take one of the following types:
        /// 
        /// * RegisterIn
        /// * AuthenticateIn
        /// * DeregisterIn
        /// </summary>
        public T Args { get; set; }

        /// <summary>
        /// List of UAF extensions. For the definition of the Extension dictionary see [UAFProtocol].
        /// </summary>
        public Extension[] Exts { get; set; }
    }
}
