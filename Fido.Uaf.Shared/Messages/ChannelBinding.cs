using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// ChannelBinding contains channel binding information [RFC5056](http://www.ietf.org/rfc/rfc5056.txt).
    /// </summary>
    public class ChannelBinding
    {
        /// <summary>
        /// The field `serverEndPoint` MUST be set to the base64url-encoded hash
        /// of the TLS server certificate if this is available.The hash function
        /// MUST be selected as follows:
        /// 
        ///  * if the certificate's `signatureAlgorithm` uses a single hash function
        ///    and that hash function is either MD5 [RFC1321](http://www.ietf.org/rfc/rfc1321.txt)
        ///    or SHA-1 [RFC6234](http://www.ietf.org/rfc/rfc6234.txt), then use SHA-256
        ///    [FIPS180-4](http://csrc.nist.gov/publications/fips/fips180-4/fips-180-4.pdf);
        ///  * if the certificate's `signatureAlgorithm` uses a single hash function
        ///    and that hash function is neither MD5 nor SHA-1, then use the hash function
        ///    associated with the certificate's `signatureAlgorithm`;
        ///  * if the certificate's `signatureAlgorithm` uses no hash functions,
        ///    or uses multiple hash functions, then this channel binding type's
        ///    channel bindings are undefined at this time(updates to this channel
        ///    binding type may occur to address this issue if it ever arises)
        /// 
        /// This field MUST be absent if the TLS server certificate is not available to
        /// the processing entity (e.g., the FIDO UAF Client) or the hash function cannot
        /// be determined as described.
        /// </summary>
        [JsonProperty("serverEndPoint")]
        public string ServerEndPoint { get; set; }

        /// <summary>
        /// This field MUST be absent if the TLS server certificate is not available
        /// to the FIDO UAF Client.
        /// 
        /// This field MUST be set to the base64url-encoded, DER-encoded TLS server
        /// certificate, if this data is available to the FIDO UAF Client.
        /// 
        /// </summary>
        [JsonProperty("tlsServerCertificate")]
        public string TlsServerCertificate { get; set; }

        /// <summary>
        /// MUST be set to the base64url-encoded TLS channel `Finished` structure.
        /// It MUST, however, be absent, if this data is not available to the FIDO UAF Client
        /// [RFC5929](http://www.ietf.org/rfc/rfc5929.txt).
        /// </summary>
        [JsonProperty("tlsUnique")]
        public string TlsUnique { get; set; }

        /// <summary>
        /// MUST be absent if the client TLS stack doesn't provide TLS ChannelID
        /// [ChannelID](http://tools.ietf.org/html/draft-balfanz-tls-channelid)
        /// information to the processing entity (e.g., the web browser or client application).
        /// 
        /// MUST be set to "unused" if TLS ChannelID information is supported by
        /// the client-side TLS stack but has not been signaled by the TLS(web) server.
        /// 
        /// Otherwise, it MUST be set to the base64url-encoded serialized
        /// [RFC4627](https://tools.ietf.org/html/rfc4627) `JwkKey` structure using UTF-8 encoding.
        /// </summary>
        [JsonProperty("cid_pubkey")]
        public string CidPubkey { get; set; }
    }
}