using System;

namespace Fido.Uaf.Shared.AuthenticatorCharacteristics
{
    /// <summary>
    /// The KEY_PROTECTION constants are flags in a bit field represented as a 16 bit long integer.
    /// They describe the method an authenticator uses to protect the private key material for FIDO registrations.
    /// These constants are used in the authoritative metadata for an authenticator,reported and queried
    /// through the UAF Discovery APIs, and used to form authenticator policies in UAF protocol messages.
    /// 
    /// When used in metadata describing an authenticator, several of these flags are exclusive of others
    /// (i.e.can not be combined) - the certified metadata may have at most one of the mutually exclusive
    /// bits set to 1. When used in authenticator policy, any bit may be set to 1, e.g.to indicate that
    /// a server is willing to accept authenticators using either KEY_PROTECTION_SOFTWARE or KEY_PROTECTION_HARDWARE.
    /// </summary>
    /// <note>
    /// These flags must be set according to the effective security of the keys, in order to follow
    /// the assumptions made in [FIDOSecRef]. For example, if a key is stored in a secure element
    /// but software running on the FIDO User Device could call a function in the secure element
    /// to export the key either in the clear or using an arbitrary wrapping key, then the effective
    /// security is KEY_PROTECTION_SOFTWARE and not KEY_PROTECTION_SECURE_ELEMENT. 
    /// </note>
    [Flags]
    public enum KeyProtectionTypes
    {
        /// <summary>
        /// KEY_PROTECTION_SOFTWARE 0x01
        /// 
        /// This flag MUST be set if the authenticator uses software-based key management.
        /// Exclusive in authenticator metadata with KEY_PROTECTION_HARDWARE, KEY_PROTECTION_TEE,
        /// KEY_PROTECTION_SECURE_ELEMENT
        /// </summary>
        KeyProtectionSoftware = 0x01,

        /// <summary>
        /// KEY_PROTECTION_HARDWARE 0x02
        /// 
        /// This flag SHOULD be set if the authenticator uses hardware-based key management.
        /// Exclusive in authenticator metadata with KEY_PROTECTION_SOFTWARE
        /// </summary>
        KeyProtectionHardware = 0x02,

        /// <summary>
        /// KEY_PROTECTION_TEE 0x04
        /// 
        /// This flag SHOULD be set if the authenticator uses the Trusted Execution Environment [TEE](https://www.globalplatform.org/specifications.asp)
        /// for key management. In authenticator metadata, this flag should be set in conjunction
        /// with KEY_PROTECTION_HARDWARE. Exclusive in authenticator metadata with KEY_PROTECTION_SOFTWARE,
        /// KEY_PROTECTION_SECURE_ELEMENT.
        /// </summary>
        KeyProtectionTee = 0x04,

        /// <summary>
        /// KEY_PROTECTION_SECURE_ELEMENT 0x08
        /// 
        /// This flag SHOULD be set if the authenticator uses a Secure Element [SecureElement](https://www.globalplatform.org/specifications.asp)
        /// for key management. In authenticator metadata, this flag should be set in conjunction
        /// with KEY_PROTECTION_HARDWARE. Exclusive in authenticator metadata with KEY_PROTECTION_TEE,
        /// KEY_PROTECTION_SOFTWARE.
        /// </summary>
        KeyProtectionSecureElement = 0x08,

        /// <summary>
        /// KEY_PROTECTION_REMOTE_HANDLE 0x10
        /// 
        /// This flag MUST be set if the authenticator does not store (wrapped) UAuth keys
        /// at the client, but relies on a server-provided key handle. This flag MUST be set
        /// in conjunction with one of the other KEY_PROTECTION flags to indicate how
        /// the local key handle wrapping key and operations are protected. Servers MAY unset
        /// this flag in authenticator policy if they are not prepared to store and return key handles,
        /// for example, if they have a requirement to respond indistinguishably to authentication
        /// attempts against userIDs that do and do not exist.
        /// </summary>
        KeyProtectionRemoteHandle = 0x10,
    }
}
