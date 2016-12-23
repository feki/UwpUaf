using Windows.Storage.Streams;

namespace UwpUaf.Authenticator
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterResponse
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

        internal RegisterResponse(IBuffer signedChallenge, IBuffer publicKey, IBuffer attestation)
        {
            SignedChallenge = signedChallenge;
            PublicKey = publicKey;
            Attestation = attestation;
        }
    }
}