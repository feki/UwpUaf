namespace Fido.Uaf.Shared.AuthenticatorCharacteristics
{
    /// <summary>
    /// The UAF_ALG_KEY constants are 16 bit long integers indicating the specific Public Key
    /// algorithm and encoding.
    /// </summary>
    /// <note>
    /// FIDO UAF supports RAW and DER encodings in order to allow small footprint authenticator
    /// implementations. By definition, the authenticator must encode the public key as part of the
    /// registration assertion.
    /// </note>
    public enum PublicKeyRepresentationFormats
    {
        /// <summary>
        /// UAF_ALG_KEY_ECC_X962_RAW 0x100
        ///
        /// Raw ANSI X9.62 formatted Elliptic Curve public key[SEC1].
        ///
        /// I.e. [0x04, X(32 bytes), Y(32 bytes)]. Where the byte 0x04 denotes the uncompressed
        /// point compression method.
        /// </summary>
        UafAlgKeyEccX962Raw = 0x100,

        /// <summary>
        /// UAF_ALG_KEY_ECC_X962_DER 0x101
        ///
        /// DER [ITU-X690-2008] encoded ANSI X.9.62 formatted
        /// SubjectPublicKeyInfo [RFC5480] specifying an elliptic curve public key.
        ///
        /// I.e.a DER encoded SubjectPublicKeyInfo as defined in [RFC5480].
        ///
        /// Authenticator implementations MUST generate namedCurve in the ECParameters object which
        /// is included in the AlgorithmIdentifier.A FIDO UAF Server MUST accept namedCurve in the
        /// ECParameters object which is included in the AlgorithmIdentifier
        /// </summary>
        UafAlgKeyEccX962Der = 0x101,

        /// <summary>
        /// UAF_ALG_KEY_RSA_2048_PSS_RAW 0x102
        ///
        /// Raw encoded RSASSA-PSS public key[RFC3447]. The default parameters according to[RFC4055]
        /// MUST be assumed, i.e.
        ///
        /// * Mask Generation Algorithm MGF1 with SHA256
        /// * Salt Length of 32 bytes, i.e.the length of a SHA256 hash value.
        /// * Trailer Field value of 1, which represents the trailer field with hexadecimal value 0xBC.
        ///
        /// That is, [n(256 bytes), e(N - n bytes)]. Where N is the total length of the field.
        ///
        /// This total length should be taken from the object containing this key, e.g.the TLV
        /// encoded field.
        /// </summary>
        UafAlgKeyRsa2048PssRaw = 0x102,

        /// <summary>
        /// UAF_ALG_KEY_RSA_2048_PSS_DER 0x103
        ///
        /// ASN.1 DER [ITU-X690-2008] encoded RSASSA-PSS [RFC3447] public key[RFC4055].
        ///
        /// The default parameters according to[RFC4055] MUST be assumed, i.e.
        ///
        /// * Mask Generation Algorithm MGF1 with SHA256
        /// * Salt Length of 32 bytes, i.e.the length of a SHA256 hash value.
        /// * Trailer Field value of 1, which represents the trailer field with hexadecimal value 0xBC.
        ///
        /// That is, a DER encoded SEQUENCE { n INTEGER, e INTEGER }.
        /// </summary>
        UafAlgKeyRsa2048PssDer = 0x103,
    }
}
