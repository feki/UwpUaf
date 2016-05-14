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

        internal RegisterResponse(IBuffer signedChallenge, IBuffer publicKey)
        {
            SignedChallenge = signedChallenge;
            PublicKey = publicKey;
        }
    }
}