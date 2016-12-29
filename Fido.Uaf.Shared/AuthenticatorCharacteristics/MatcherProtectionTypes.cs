using System;

namespace Fido.Uaf.Shared.AuthenticatorCharacteristics
{
    /// <summary>
    /// The MATCHER_PROTECTION constants are flags in a bit field represented as a 16 bit long integer.
    /// They describe the method an authenticator uses to protect the matcher that performs user verification.
    /// These constants are used in the authoritative metadata for an authenticator, reported and queried
    /// through the UAF Discovery APIs, and used to form authenticator policies in UAF protocol messages.
    /// </summary>
    /// <note>
    /// These flags must be set according to the effective security of the matcher, in order to follow
    /// the assumptions made in [FIDOSecRef]. For example, if a passcode based matcher is implemented in
    /// a secure element, but the passcode is expected to be provided as unauthenticated parameter,
    /// then the effective security is MATCHER_PROTECTION_SOFTWARE and not MATCHER_PROTECTION_ON_CHIP.
    /// </note>
    [Flags]
    public enum MatcherProtectionTypes
    {
        /// <summary>
        /// MATCHER_PROTECTION_SOFTWARE 0x01
        ///
        /// This flag MUST be set if the authenticator's matcher is running in software.
        /// Exclusive in authenticator metadata with MATCHER_PROTECTION_TEE, MATCHER_PROTECTION_ON_CHIP.
        /// </summary>
        MatcherProtectionSoftware = 0x01,

        /// <summary>
        /// MATCHER_PROTECTION_TEE 0x02
        ///
        /// This flag SHOULD be set if the authenticator's matcher is running inside the Trusted Execution Environment [TEE].
        /// Exclusive in authenticator metadata with MATCHER_PROTECTION_SOFTWARE, MATCHER_PROTECTION_ON_CHIP.
        /// </summary>
        MatcherProtectionTee = 0x02,

        /// <summary>
        /// MATCHER_PROTECTION_ON_CHIP 0x04
        ///
        /// This flag SHOULD be set if the authenticator's matcher is running on the chip.
        /// Exclusive in authenticator metadata with MATCHER_PROTECTION_TEE, MATCHER_PROTECTION_SOFTWARE
        /// </summary>
        MatcherProtectionOnChip = 0x04,
    }
}
