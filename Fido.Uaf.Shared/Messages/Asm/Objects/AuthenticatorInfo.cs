using Fido.Uaf.Shared.AuthenticatorCharacteristics;
using Fido.Uaf.Shared.Tlv;
using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    /// <summary>
    /// Authenticator info.
    /// </summary>
    public class AuthenticatorInfo
    {
        /// <summary>
        /// Authenticator index. Unique, within the scope of all authenticators reported by the ASM,
        /// index referring to an authenticator. This index is used by the UAF Client to refer to
        /// the appropriate authenticator in further requests.
        /// </summary>
        [JsonProperty("authenticatorIndex")]
        public short AuthenticatorIndex { get; set; }

        /// <summary>
        /// A list of ASM Versions that this authenticator can be used with. For the definition of
        /// the Version dictionary see [UAFProtocol].
        /// </summary>
        [JsonProperty("asmVersions")]
        public Version[] AsmVersions { get; set; }

        /// <summary>
        /// Indicates whether a user is enrolled with this authenticator. Authenticators which don't
        /// have user verification technology MUST always return true. Bound authenticators which
        /// support different profiles per operating system (OS) user MUST report enrollment status
        /// for the current OS user.
        /// </summary>
        [JsonProperty("isUserEnrolled")]
        public bool IsUserEnrolled { get; set; }

        /// <summary>
        /// A boolean value indicating whether the authenticator has its own settings. If so, then a
        /// FIDO UAF Client can launch these settings by sending a OpenSettings request.
        /// </summary>
        [JsonProperty("hasSettings")]
        public bool HasSettings { get; set; }

        /// <summary>
        /// The "Authenticator Attestation ID" (AAID), which identifies the type and batch of the
        /// authenticator. See [UAFProtocol] for the definition of the AAID structure.
        /// </summary>
        [JsonProperty("aaid")]
        public string Aaid { get; set; }

        /// <summary>
        /// The assertion scheme the authenticator uses for attested data and signatures.
        /// 
        /// AssertionScheme identifiers are defined in the UAF Protocol specification[UAFProtocol].
        /// </summary>
        [JsonProperty("assertionScheme")]
        public string AssertionScheme { get; set; }

        /// <summary>
        /// Indicates the authentication algorithm that the authenticator uses. Authentication
        /// algorithm identifiers are defined in are defined in [UAFRegistry] with UAF_ALG prefix.
        /// </summary>
        [JsonProperty("authenticationAlgorithm")]
        public AuthenticationAlgorithms AuthenticationAlgorithm { get; set; }

        /// <summary>
        /// Indicates attestation types supported by the authenticator. Attestation type TAGs are
        /// defined in [UAFRegistry] with TAG_ATTESTATION prefix.
        /// </summary>
        [JsonProperty("attestationTypes")]
        public short AttestationTypes { get; set; }

        /// <summary>
        /// A set of bit flags indicating the user verification method(s) supported by the
        /// authenticator. The values are defined by the USER_VERIFY constants in [UAFRegistry].
        /// </summary>
        [JsonProperty("userVerification")]
        public UserVerificationMethods UserVerification { get; set; }

        /// <summary>
        /// A set of bit flags indicating the key protections used by the authenticator. The values
        /// are defined by the KEY_PROTECTION constants in [UAFRegistry].
        /// </summary>
        [JsonProperty("keyProtection")]
        public KeyProtectionTypes KeyProtection { get; set; }

        /// <summary>
        /// A set of bit flags indicating the matcher protections used by the authenticator. The
        /// values are defined by the MATCHER_PROTECTION constants in [UAFRegistry].
        /// </summary>
        [JsonProperty("matcherProtection")]
        public MatcherProtectionTypes MatcherProtection { get; set; }

        /// <summary>
        /// A set of bit flags indicating how the authenticator is currently connected to the system
        /// hosting the FIDO UAF Client software. The values are defined by the ATTACHMENT_HINT
        /// constants defined in [UAFRegistry].
        /// </summary>
        /// <note>
        /// Because the connection state and topology of an authenticator may be transient, these
        /// values are only hints that can be used by server-supplied policy to guide the user
        /// experience, e.g. to prefer a device that is connected and ready for authenticating or
        /// confirming a low-value transaction, rather than one that is more secure but requires
        /// more user effort. These values are not reflected in authenticator metadata and cannot be
        /// relied on by the relying party, although some models of authenticator may provide
        /// attested measurements with similar semantics as part of UAF protocol messages.
        /// </note>
        [JsonProperty("attachmentHint")]
        public AttachmentHints AttachmentHint { get; set; }

        /// <summary>
        /// Indicates whether the authenticator can be used only as a second factor.
        /// </summary>
        [JsonProperty("isSecondFactorOnly")]
        public bool IsSecondFactorOnly { get; set; }

        /// <summary>
        /// Indicates whether this is a roaming authenticator or not.
        /// </summary>
        [JsonProperty("isRoamingAuthenticator")]
        public bool IsRoamingAuthenticator { get; set; }

        /// <summary>
        /// List of supported UAF extension Ids. MAY be an empty list.
        /// </summary>
        [JsonProperty("supportedExtensionIDs")]
        public string[] SupportedExtensionIds { get; set; }

        /// <summary>
        /// A set of bit flags indicating the availability and type of the authenticator's
        /// transaction confirmation display. The values are defined by the
        /// TRANSACTION_CONFIRMATION_DISPLAY constants in [UAFRegistry].
        /// 
        /// This value MUST be 0 if transaction confirmation is not supported by the authenticator.
        /// </summary>
        [JsonProperty("tcDisplay")]
        public TransactionConfirmationDisplayTypes TcDisplay { get; set; }

        /// <summary>
        /// Supported transaction content type [UAFAuthnrMetadata].
        /// 
        /// This value MUST be present if transaction confirmation is supported, i.e. TcDisplay is non-zero.
        /// </summary>
        [JsonProperty("tcDisplayContentType")]
        public string TcDisplayContentType { get; set; }

        /// <summary>
        /// Supported transaction Portable Network Graphic (PNG) type [UAFAuthnrMetadata]. For the
        /// definition of the DisplayPngCharacteristicsDescriptor structure see [UAFAuthnrMetadata].
        /// 
        /// This list MUST be present if transaction confirmation is supported, i.e.tcDisplay is non-zero.
        /// </summary>
        [JsonProperty("tcDisplayPngCharacteristics")]
        public DisplayPngCharacteristicsDescriptor[] TcDisplayPngCharacteristics { get; set; }

        /// <summary>
        /// A human-readable short title for the authenticator. It should be localized for the
        /// current locale.
        /// </summary>
        /// <note>
        /// If the ASM doesn't return a title, the FIDO UAF Client must provide a title to the
        /// calling App. See section "Authenticator interface" in [UAFAppAPIAndTransport].
        /// </note>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Human-readable longer description of what the authenticator represents.
        /// </summary>
        /// <note>
        /// This text should be localized for current locale.
        /// 
        /// The text is intended to be displayed to the user.It might deviate from the description
        /// specified in the metadata statement for the authenticator [UAFAuthnrMetadata].
        /// 
        /// If the ASM doesn't return a description, the FIDO UAF Client will provide a description
        /// to the calling application. See section "Authenticator interface" in [UAFAppAPIAndTransport].
        /// </note>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Portable Network Graphic (PNG) format image file representing the icon encoded as a
        /// data: url [RFC2397].
        /// </summary>
        /// <note>
        /// If the ASM doesn't return an icon, the FIDO UAF Client will provide a default icon to
        /// the calling application. See section "Authenticator interface" in [UAFAppAPIAndTransport].
        /// </note>
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}