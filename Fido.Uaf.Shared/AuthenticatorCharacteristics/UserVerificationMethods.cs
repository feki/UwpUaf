using System;

namespace Fido.Uaf.Shared.AuthenticatorCharacteristics
{
    /// <summary>
    /// The USER_VERIFY constants are flags in a bitfield represented as a 32 bit long integer.
    /// They describe the methods and capabilities of an UAF authenticator for locally verifying a user.
    /// The operational details of these methods are opaque to the server.
    /// These constants are used in the authoritative metadata for an authenticator,
    /// reported and queried through the UAF Discovery APIs, and used to form authenticator
    /// policies in UAF protocol messages.
    ///
    /// All user verification methods must be performed locally by the authenticator
    /// in order to meet FIDO privacy principles.
    /// </summary>
    [Flags]
    public enum UserVerificationMethods
    {
        /// <summary>
        /// USER_VERIFY_PRESENCE 0x01
        /// 
        /// This flag MUST be set if the authenticator is able to confirm user presence in any fashion.
        /// If this flag and no other is set for user verification, the guarantee is only that
        /// the authenticator cannot be operated without some human intervention,
        /// not necessarily that the presence verification provides any level of authentication of the human's identity.
        /// (e.g. a device that requires a touch to activate)
        /// </summary>
        UserVerifyPresence = 0x01,

        /// <summary>
        /// USER_VERIFY_FINGERPRINT 0x02
        /// 
        /// This flag MUST be set if the authenticator uses any type of measurement of a fingerprint for user verification.
        /// </summary>
        UserVerifyFingerprint = 0x02,

        /// <summary>
        /// USER_VERIFY_PASSCODE 0x04
        /// 
        /// This flag MUST be set if the authenticator uses a local-only passcode
        /// (i.e. a passcode not known by the server) for user verification.
        /// </summary>
        UserVerifyPasscode = 0x04,

        /// <summary>
        /// USER_VERIFY_VOICEPRINT 0x08
        /// 
        /// This flag MUST be set if the authenticator uses a voiceprint
        /// (also known as speaker recognition) for user verification.
        /// </summary>
        UserVerifyVoiceprint = 0x08,

        /// <summary>
        /// USER_VERIFY_FACEPRINT 0x10
        /// 
        /// This flag MUST be set if the authenticator uses any manner of face recognition to verify the user.
        /// </summary>
        UserVerifyFaceprint = 0x10,

        /// <summary>
        /// USER_VERIFY_LOCATION 0x20
        /// 
        /// This flag MUST be set if the authenticator uses any form of location sensor
        /// or measurement for user verification.
        /// </summary>
        UserVerifyLocation = 0x20,

        /// <summary>
        /// USER_VERIFY_EYEPRINT 0x40
        /// 
        /// This flag MUST be set if the authenticator uses any form of eye biometrics
        /// for user verification.
        /// </summary>
        UserVerifyEyeprint = 0x40,

        /// <summary>
        /// USER_VERIFY_PATTERN 0x80
        /// 
        /// This flag MUST be set if the authenticator uses a drawn pattern for user verification.
        /// </summary>
        UserVerifyPattern = 0x80,

        /// <summary>
        /// USER_VERIFY_HANDPRINT 0x100
        /// 
        /// This flag MUST be set if the authenticator uses any measurement of a full hand
        /// (including palm-print, hand geometry or vein geometry) for user verification.
        /// </summary>
        UserVerifyHandprint = 0x100,

        /// <summary>
        /// USER_VERIFY_NONE 0x200
        /// 
        /// This flag MUST be set if the authenticator will respond without any user interaction
        /// (e.g. Silent Authenticator). 
        /// </summary>
        UserVerifyNone = 0x200,

        /// <summary>
        /// USER_VERIFY_ALL 0x400
        /// 
        /// If an authenticator sets multiple flags for user verification types,
        /// it MAY also set this flag to indicate that all verification methods will be enforced
        /// (e.g. faceprint AND voiceprint). If flags for multiple user verification methods are
        /// set and this flag is not set, verification with only one is necessary (e.g. fingerprint OR passcode).
        /// </summary>
        UserVerifyAll = 0x400,
    }
}
