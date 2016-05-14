namespace Fido.Uaf.Shared.Messages.Asm
{
    /// <summary>
    /// Status code.
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// UAF_ASM_STATUS_OK of type short
        /// 
        /// No error condition encountered.
        /// </summary>
        UafAsmStatusOk = 0x00,

        /// <summary>
        /// UAF_ASM_STATUS_ERROR of type short
        /// 
        /// An unknown error has been encountered during the processing.
        /// </summary>
        UafAsmStatusError = 0x01,

        /// <summary>
        /// UAF_ASM_STATUS_ACCESS_DENIED of type short
        /// 
        /// Access to this request is denied.
        /// </summary>
        UafAsmStatusAccessDenied = 0x02,

        /// <summary>
        /// UAF_ASM_STATUS_USER_CANCELLED of type short
        /// 
        /// Indicates that user explicitly canceled the request.
        /// </summary>
        UafAsmStatusUserCancelled = 0x03,
    }
}
