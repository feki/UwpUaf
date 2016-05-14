using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage.Streams;

namespace UwpUaf.Authenticator
{
    /// <summary>
    /// 
    /// </summary>
    public class UwpUafAuthenticator : IUwpUafAuthenticator
    {
        public async Task<RegisterResponse> RegisterAsync(string appId, IBuffer challenge)
        {
            await CheckSupportAsync();

            // Create a new KeyCredential for the user on the device.
            var keyCredentialRetrievalResult = await KeyCredentialManager.RequestCreateAsync(appId, KeyCredentialCreationOption.ReplaceExisting);
            keyCredentialRetrievalResult.CheckStatus();

            // User has autheniticated with Windows Hello and the key credential is created.
            KeyCredential userKey = keyCredentialRetrievalResult.Credential;

            var publicKey = userKey.RetrievePublicKey();
            var signedChallenge = await userKey.SignAsync(challenge);

            return new RegisterResponse(signedChallenge, publicKey);
        }

        public async Task<SignResponse> SignAsync(string appId, IBuffer challenge)
        {
            await CheckSupportAsync();

            var keyCredentialRetrievalResult = await KeyCredentialManager.OpenAsync(appId);
            keyCredentialRetrievalResult.CheckStatus();

            KeyCredential credential = keyCredentialRetrievalResult.Credential;
            var signedChallenge = await credential.SignAsync(challenge);

            return new SignResponse(signedChallenge);
        }

        public async Task UnregisterAsync(string appId)
        {
            await CheckSupportAsync();

            await KeyCredentialManager.DeleteAsync(appId);
        }

        private async Task CheckSupportAsync()
        {
            if(!await KeyCredentialManager.IsSupportedAsync())
            {
                throw new NotSupportedException("Windows Hello is not set up.");
            }
        }
    }
}
