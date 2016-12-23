using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace UwpUaf.Authenticator
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUwpUafAuthenticator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> IsSupportedAsync();

        /// <summary>
        /// 
        /// </summary>
        Task<RegisterResponse> RegisterAsync(string appId, IBuffer challenge);

        /// <summary>
        /// 
        /// </summary>
        Task<SignResponse> SignAsync(string appId, IBuffer challenge);

        /// <summary>
        /// 
        /// </summary>
        Task UnregisterAsync(string appId);
    }
}