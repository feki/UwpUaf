using Windows.Storage.Streams;

namespace UwpUaf.Authenticator
{
    /// <summary>
    ///
    /// </summary>
    public class SignResponse
    {
        /// <summary>
        ///
        /// </summary>
        public IBuffer SignedChallenge { get; }

        /// <summary>
        ///
        /// </summary>
        public IBuffer PublicKey { get; }

        /// <summary>
        ///
        /// </summary>
        public IBuffer Attestation { get; }

        internal SignResponse(IBuffer signedChallenge, IBuffer publicKey, IBuffer attestation)
        {
            SignedChallenge = signedChallenge;
            PublicKey = publicKey;
            Attestation = attestation;
        }
    }
}