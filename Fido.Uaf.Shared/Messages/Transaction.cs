using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    ///
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Contains the MIME Content-Type supported by the authenticator
        /// according its metadata statement (see [UAFAuthnrMetadata](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-authnr-metadata-v1.0-ps-20141208.html)).
        ///
        /// This version of the specification only supports the values `text/plain` or `image/png`.
        /// </summary>
        [JsonProperty("contentType", Required = Required.Always)]
        public string ContentType { get; set; }

        /// <summary>
        /// Contains the base64-url encoded transaction content according to
        /// the `contentType` to be shown to the user.
        ///
        /// If `contentType` is "text/plain" then the content MUST be the base64-url
        /// encoding of the ASCII encoded text with a maximum of 200 characters.
        /// </summary>
        /// <remarks>
        /// base64url(byte[1...])
        /// </remarks>
        [JsonProperty("content", Required = Required.Always)]
        public string Content { get; set; }

        /// <summary>
        /// Transaction content PNG characteristics.
        /// For the definition of the DisplayPngCharacteristicsDescriptor structure
        /// See [UAFAuthnrMetadata](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-authnr-metadata-v1.0-ps-20141208.html).
        /// This field MUST be present if the contentType is "image/png".
        /// </summary>
        [JsonProperty("tcDisplayPNGCharacteristics", NullValueHandling = NullValueHandling.Ignore)]
        public DisplayPngCharacteristicsDescriptor TcDisplayPngCharacteristics { get; set; }
    }
}
