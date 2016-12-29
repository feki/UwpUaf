namespace Fido.Uaf.Shared.AuthenticatorCharacteristics
{
    /// <summary>
    /// The TRANSACTION_CONFIRMATION_DISPLAY constants are flags in a bit field represented as a 16
    /// bit long integer. They describe the availability and implementation of a transaction
    /// confirmation display capability required for the transaction confirmation operation. These
    /// constants are used in the authoritative metadata for an authenticator, reported and queried
    /// through the UAF Discovery APIs, and used to form authenticator policies in UAF protocol
    /// messages. Refer to [UAFAuthnrCommands] for more details on the security aspects of
    /// TransactionConfirmation Display.
    /// </summary>
    public enum TransactionConfirmationDisplayTypes
    {
        /// <summary>
        /// Transaction confirmation is not supported by the authenticator.
        /// </summary>
        TransactionConfirmationDisplayNone = 0x0,

        /// <summary>
        /// TRANSACTION_CONFIRMATION_DISPLAY_ANY 0x01
        ///
        /// This flag MUST be set to indicate, that some form of transaction confirmation display is
        /// available on this authenticator.
        /// </summary>
        TransactionConfirmationDisplayAny = 0x01,

        /// <summary>
        /// TRANSACTION_CONFIRMATION_DISPLAY_PRIVILEGED_SOFTWARE 0x02.
        ///
        /// This flag MUST be set to indicate, that a software-based transaction confirmation
        /// display operating in a privileged context is available on this authenticator. A FIDO
        /// client that is capable of providing this capability MAY set this bit for all
        /// authenticators of type ATTACHMENT_HINT_INTERNAL, even if the authoritative metadata for
        /// the authenticator does not indicate this capability.
        /// </summary>
        /// <note>
        /// Software based transaction confirmation displays might be implemented within the
        /// boundaries of the ASM rather than by the authenticator itself [UAFASM].
        /// </note>
        TransactionConfirmationDisplayPrivilegedSoftware = 0x02,

        /// <summary>
        /// TRANSACTION_CONFIRMATION_DISPLAY_TEE 0x04
        ///
        /// This flag SHOULD be set to indicate that the authenticator implements a transaction
        /// confirmation display in a Trusted Execution Environment ([TEE], [TEESecureDisplay](https://www.globalplatform.org/specifications.asp)).
        /// </summary>
        TransactionConfirmationDisplayTee = 0x04,

        /// <summary>
        /// TRANSACTION_CONFIRMATION_DISPLAY_HARDWARE 0x08
        ///
        /// This flag SHOULD be set to indicate that a transaction confirmation display based on
        /// hardware assisted capabilities is available on this authenticator.
        /// </summary>
        TransactionConfirmationDisplayHardware = 0x08,

        /// <summary>
        /// TRANSACTION_CONFIRMATION_DISPLAY_REMOTE 0x10
        ///
        /// This flag SHOULD be set to indicate that the transaction confirmation display is
        /// provided on a distinct device from the FIDO User Device.
        /// </summary>
        TransactionConfirmationDisplayRemote = 0x08,
    }
}
