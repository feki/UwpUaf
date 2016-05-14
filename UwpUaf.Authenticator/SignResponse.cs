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

        internal SignResponse(IBuffer signedChallenge)
        {
            SignedChallenge = signedChallenge;
        }
    }
}