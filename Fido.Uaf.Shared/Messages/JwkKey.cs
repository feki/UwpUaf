namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// `JwkKey` representing a JSON Web Key encoding of an elliptic curve public key
    /// [JWK](http://tools.ietf.org/html/draft-ietf-jose-json-web-key-11).
    /// 
    /// This public key is the ChannelID public key minted by the client TLS stack
    /// for the particular relying party. [ChannelID](http://tools.ietf.org/html/draft-balfanz-tls-channelid)
    /// stipulates using only a particular elliptic curve, and the particular coordinate type.
    /// </summary>
    public class JwkKey
    {
        /// <summary>
        /// Denotes the key type used for Channel ID. At this time only elliptic curve
        /// is supported by [ChannelID](http://tools.ietf.org/html/draft-balfanz-tls-channelid),
        /// so it MUST be set to "EC" [JWA](http://tools.ietf.org/html/draft-ietf-jose-json-web-algorithms).
        /// </summary>
        public string kty { get; set; } = "EC";

        /// <summary>
        /// Denotes the elliptic curve on which this public key is defined. At this time only the NIST curve `secp256r1`
        /// is supported by [ChannelID](http://tools.ietf.org/html/draft-balfanz-tls-channelid),
        /// so the `crv` parameter MUST be set to "P-256".
        /// </summary>
        public string crv { get; set; } = "P-256";

        /// <summary>
        /// Contains the base64url-encoding of the x coordinate of the public key (big-endian, 32-byte value).
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// Contains the base64url-encoding of the y coordinate of the public key (big-endian, 32-byte value).
        /// </summary>
        public string Y { get; set; }
    }
}
