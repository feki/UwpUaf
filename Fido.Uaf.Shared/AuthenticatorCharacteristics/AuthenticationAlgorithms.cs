namespace Fido.Uaf.Shared.AuthenticatorCharacteristics
{
    /// <summary>
    /// The UAF_ALG_SIGN constants are 16 bit long integers indicating the specific signature
    /// algorithm and encoding.
    /// </summary>
    /// <note>
    /// FIDO UAF supports RAW and DER signature encodings in order to allow small footprint
    /// authenticator implementations.
    /// </note>
    public enum AuthenticationAlgorithms
    {
        /// <summary>
        /// UAF_ALG_SIGN_SECP256R1_ECDSA_SHA256_RAW 0x01
        ///
        /// An ECDSA signature on the NIST secp256r1 curve which MUST have raw R and S buffers,
        /// encoded in big-endian order.
        ///
        /// I.e. [R(32 bytes), S(32 bytes)]
        /// </summary>
        UafAlgSignSecp256r1EcdsaSha256Raw = 0x01,

        /// <summary>
        /// UAF_ALG_SIGN_SECP256R1_ECDSA_SHA256_DER 0x02
        /// DER [ITU-X690-2008] encoded ECDSA signature [RFC5480] on the NIST secp256r1 curve.
        ///
        /// I.e. a DER encoded SEQUENCE { r INTEGER, s INTEGER }
        /// </summary>
        UafAlgSignSecp256r1EcdsaSha256Der = 0x02,

        /// <summary>
        /// UAF_ALG_SIGN_RSASSA_PSS_SHA256_RAW 0x03
        ///
        /// RSASSA-PSS [RFC3447] signature MUST have raw S buffers, encoded in big-endian order
        /// [RFC4055][RFC4056]. The default parameters as specified in [RFC4055] MUST be assumed, i.e
        ///
        /// * Mask Generation Algorithm MGF1 with SHA256
        /// * Salt Length of 32 bytes, i.e. the length of a SHA256 hash value.
        /// * Trailer Field value of 1, which represents the trailer field with hexadecimal value 0xBC.
        ///
        /// I.e. [S(256 bytes)]
        /// </summary>
        UafAlgSignRsassaPssSha256Raw = 0x03,

        /// <summary>
        /// UAF_ALG_SIGN_RSASSA_PSS_SHA256_DER 0x04
        ///
        /// DER [ITU-X690-2008] encoded OCTET STRING (not BIT STRING!) containing the RSASSA-PSS
        /// [RFC3447] signature [RFC4055] [RFC4056]. The default parameters as specified in
        /// [RFC4055] MUST be assumed, i.e.
        ///
        /// * Mask Generation Algorithm MGF1 with SHA256
        /// * Salt Length of 32 bytes, i.e. the length of a SHA256 hash value.
        /// * Trailer Field value of 1, which represents the trailer field with hexadecimal value 0xBC.
        ///
        /// I.e. a DER encoded OCTET STRING (including its tag and length bytes).
        /// </summary>
        UafAlgSignRsassaPssSha256Der = 0x04,

        /// <summary>
        /// UAF_ALG_SIGN_SECP256K1_ECDSA_SHA256_RAW 0x05
        ///
        /// An ECDSA signature on the secp256k1 curve which MUST have raw R and S buffers, encoded
        /// in big-endian order.
        ///
        /// I.e.[R(32 bytes), S(32 bytes)]
        /// </summary>
        UafAlgSignSecp256k1EcdsaSha256Raw = 0x05,

        /// <summary>
        /// UAF_ALG_SIGN_SECP256K1_ECDSA_SHA256_DER 0x06
        ///
        /// DER [ITU-X690-2008] encoded ECDSA signature [RFC5480] on the secp256k1 curve.
        ///
        /// I.e. a DER encoded SEQUENCE { r INTEGER, s INTEGER }
        /// </summary>
        UafAlgSignSecp256k1EcdsaSha256Der = 0x06,
    }
}
