using System;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage.Streams;

namespace UwpUaf.Asm.Shared
{
    /// <summary>
    ///
    /// </summary>
    internal static class CredentialsExtentions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyCredential"></param>
        /// <param name="challenge"></param>
        /// <returns></returns>
        public static async Task<IBuffer> SignAsync(this KeyCredential keyCredential, IBuffer challenge)
        {
            var keyCredentialOperationResult = await keyCredential.RequestSignAsync(challenge);
            keyCredentialOperationResult.CheckStatus();

            return keyCredentialOperationResult.Result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyCredentialRetrievalResult"></param>
        public static void CheckStatus(this KeyCredentialRetrievalResult keyCredentialRetrievalResult)
        {
            CheckStatus(keyCredentialRetrievalResult.Status);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyCredentialOperationResult"></param>
        public static void CheckStatus(this KeyCredentialOperationResult keyCredentialOperationResult)
        {
            CheckStatus(keyCredentialOperationResult.Status);
        }

        private static void CheckStatus(KeyCredentialStatus status)
        {
            switch (status)
            {
                case KeyCredentialStatus.UnknownError:
                    throw new Exception("Unknown error.");
                case KeyCredentialStatus.NotFound:
                    throw new Exception("Not found. To proceed, Windows Hello needs to be configured in Windows Settings (Accounts -> Sign-in options).");
                case KeyCredentialStatus.UserCanceled:
                case KeyCredentialStatus.UserPrefersPassword:
                case KeyCredentialStatus.CredentialAlreadyExists:
                case KeyCredentialStatus.SecurityDeviceLocked:
                    // TODO: remove
                    break;
                default:
                    break;
            }
        }
    }
}
