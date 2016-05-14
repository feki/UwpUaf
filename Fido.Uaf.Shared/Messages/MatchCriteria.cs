namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Represents the matching criteria to be used in the server policy.
    /// 
    /// The `MatchCriteria` object is considered to match an authenticator,
    /// if all fields in the object are considered to match (as indicated in the particular fields).
    /// </summary>
    public class MatchCriteria
    {
        /// <summary>
        /// List of AAIDs, causing matching to be restricted to certain AAIDs.
        /// 
        /// The match succeeds if at least one AAID entry in this array matches
        /// `AuthenticatorInfo.aaid` [UAFASM](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-asm-api-v1.0-ps-20141208.html).
        /// </summary>
        /// <note>
        /// This field corresponds to `MetadataStatement.aaid` [UAFAuthnrMetadata](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-authnr-metadata-v1.0-ps-20141208.html).
        /// </note>
        public string[] AAID { get; set; }

        /// <summary>
        /// The VendorIds causing matching to be restricted to authenticator models
        /// of the given vendor. The first 4 characters of the AAID are the vendorID (see `AAID`)).
        /// 
        /// The match succeeds if at least one entry in this array matches the first
        /// 4 characters of the AuthenticatorInfo.aaid [UAFASM](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-asm-api-v1.0-ps-20141208.html).
        /// </summary>
        /// <note>
        /// This field corresponds to the first 4 characters of `MetadataStatement.aaid`
        /// [UAFAuthnrMetadata](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-authnr-metadata-v1.0-ps-20141208.html).
        /// </note>
        public string[] VendorIds { get; set; }

        /// <summary>
        /// A list of authenticator KeyIds causing matching to be restricted to a given
        /// set of `KeyId` instances. (see [UAFRegistry](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-reg-v1.0-ps-20141208.html)).
        /// 
        /// This match succeeds if at least one entry in this array matches.
        /// </summary>
        /// <note>
        /// This field corresponds to AppRegistration.keyIDs [UAFASM](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-asm-api-v1.0-ps-20141208.html).
        /// </note>
        public string[] KeyIds { get; set; }

        /// <summary>
        /// A set of 32 bit flags which may be set if matching should be restricted by the user
        /// verification method (see [UAFRegistry](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-reg-v1.0-ps-20141208.html)).
        /// </summary>
        public ulong UserVerification { get; set; }

        /// <summary>
        /// A set of 16 bit flags which may be set if matching should be restricted
        /// by the key protections used (see [UAFRegistry]).
        /// 
        /// This match succeeds, if at least one of the bit flags matches the value
        /// of `AuthenticatorInfo.keyProtection` [UAFASM].
        /// </summary>
        public ushort KeyProtection { get; set; }

        /// <summary>
        /// A set of 16 bit flags which may be set if matching should be restricted
        /// by the matcher protection (see [UAFRegistry]).
        /// 
        /// The match succeeds if at least one of the bit flags matches the value
        /// of `AuthenticatorInfo.matcherProtection` [UAFASM].
        /// </summary>
        public ushort MatcherProtection { get; set; }

        /// <summary>
        /// A set of 32 bit flags which may be set if matching should be restricted
        /// by the authenticator attachment mechanism (see [UAFRegistry]).
        /// 
        /// This field is considered to match, if at least one of the bit flags matches
        /// the value of `AuthenticatorInfo.attachmentHint` [UAFASM].
        /// </summary>
        public ulong AttachmentHint { get; set; }

        /// <summary>
        /// A set of 16 bit flags which may be set if matching should be restricted
        /// by the transaction confirmation display availability and type. (see [UAFRegistry]).
        /// 
        /// This match succeeds if at least one of the bit flags matches the value
        /// of `AuthenticatorInfo.tcDisplay` [UAFASM].
        /// </summary>
        public ushort TcDisplay { get; set; }

        /// <summary>
        /// An array containing values of supported authentication algorithm TAG values
        /// (see [UAFRegistry], prefix `UAF_ALG_SIGN`) if matching should be restricted
        /// by the supported authentication algorithms.
        /// 
        /// This match succeeds if at least one entry in this array matches
        /// the `AuthenticatorInfo.authenticationAlgorithm` [UAFASM].
        /// </summary>
        public ushort[] AuthenticationAlgorithms { get; set; }

        /// <summary>
        /// A list of supported assertion schemes if matching should be
        /// restricted by the supported schemes.
        /// 
        /// See section UAF Supported Assertion Schemes (https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-protocol-v1.0-ps-20141208.html#uaf-supported-assertion-schemes) for details.
        /// 
        /// This match succeeds if at least one entry in this array matches
        /// `AuthenticatorInfo.assertionScheme` [UAFASM].
        /// </summary>
        public string[] AssertionSchemes { get; set; }

        /// <summary>
        /// An array containing the preferred attestation TAG values
        /// (see [UAFRegistry], prefix `TAG_ATTESTATION`). The order of
        /// items MUST be preserved. The most-preferred attestation type comes first.
        /// 
        /// This match succeeds if at least one entry in this array matches
        /// one entry in `AuthenticatorInfo.attestationTypes` [UAFASM].
        /// </summary>
        public ushort[] AttestationTypes { get; set; }

        /// <summary>
        /// Contains an authenticator version number, if matching should be
        /// restricted by the authenticator version in use.
        /// 
        /// This match succeeds if the value is lower or equal to the field
        /// `AuthenticatorVersion` included in `TAG_UAFV1_REG_ASSERTION`
        /// or `TAG_UAFV1_AUTH_ASSERTION` or a corresponding value
        /// in the case of a different assertion scheme.
        /// </summary>
        public ushort AuthenticatorVersion { get; set; }

        /// <summary>
        /// Extensions for matching policy.
        /// </summary>
        public Extension[] exts { get; set; }
    }
}
