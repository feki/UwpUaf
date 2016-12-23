using System;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.AuthenticatorCharacteristics;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Fido.Uaf.Shared.Tlv;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using UwpUaf.Asm.Shared.Op.Processor;
using Windows.Security.Credentials;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace UwpUaf.Asm.Shared
{
    class HelloAuthenticator : IAuthenticator
    {
        private const string AuthenticatoAttestationPrivateKey = "ME0CAQAwEwYHKoZIzj0CAQYIKoZIzj0DAQcEMzAxAgEBBCBs0AjkwtBjQmJkbr9E6k6cgFjBcnuRb85jvzHm2H9sSqAKBggqhkjOPQMBBw==";
        private const string AuthenticatoAttestationPublicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEBK2OIVzxy0l9uAAzIcYtTZpwPP29tKXNg/Fg1m5v2obunJurX8HLbLqj7iuoMjMlqfjulVLfUrVkLoc+qzZODA==";
        private const string AuthenticatoAttestationX509Certificate = "MIICCjCCAbCgAwIBAgIgK7JjKpFsqc8+UCDr8ZGpCI+r+6DuiNwy9lEV0GmGxLMwCgYIKoZIzj0EAwIwfzELMAkGA1UEBhMCQ1oxFzAVBgNVBAgMDkN6ZWNoIFJlcHVibGljMQ0wCwYDVQQHDARCcm5vMRswGQYDVQQKDBJBSEVBRCBpVGVjLCBzLnIuby4xDTALBgNVBAsMBHNlbGYxHDAaBgNVBAMME3Rlc3RAYWhlYWQtaXRlYy5jb20wHhcNMTYxMjExMTk1NTI3WhcNMjYxMjExMTk1NTI3WjB/MQswCQYDVQQGEwJDWjEXMBUGA1UECAwOQ3plY2ggUmVwdWJsaWMxDTALBgNVBAcMBEJybm8xGzAZBgNVBAoMEkFIRUFEIGlUZWMsIHMuci5vLjENMAsGA1UECwwEc2VsZjEcMBoGA1UEAwwTdGVzdEBhaGVhZC1pdGVjLmNvbTBZMBMGByqGSM49AgEGCCqGSM49AwEHA0IABAStjiFc8ctJfbgAMyHGLU2acDz9vbSlzYPxYNZub9qG7pybq1/By2y6o+4rqDIzJan47pVS31K1ZC6HPqs2TgwwCgYIKoZIzj0EAwIDSAAwRQIhAKWnrz+iAm1oQaiW+L/ZncDwxiOAlVHpHbDBB13TR6q0AiAvzNJrgpEXcIfAz18q9hSMBSgu0LFnmTZVj/95ALrFow==";

        const string ICON = "data:image/png;base64,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";

        private static HelloAuthenticator instance = null;

        private string _keyId;

        HelloAuthenticator()
        {

        }

        public static HelloAuthenticator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HelloAuthenticator();
                }

                return instance;
            }
        }

        public string Aaid => "0000#0000";

        public string AssertionScheme => AssertionSchemes.UAFV1TLV;

        public string KeyId
        {
            get
            {
                if (_keyId == null)
                {
                    var rs = new StringBuilder();
                    var rnd = new byte[16];
                    var random = new SecureRandom();

                    random.NextBytes(rnd);
                    rs.Append("$2a$10$");
                    rs.Append(Convert.ToBase64String(rnd));

                    var keyId = "UwpUaf-HelloAuthenticator-Key-" + Convert.ToBase64String(Encoding.UTF8.GetBytes(rs.ToString()));

                    _keyId = Convert.ToBase64String(Encoding.UTF8.GetBytes(keyId)).TrimEnd('=')
                        .Replace('+', '-')
                        .Replace('/', '_');
                }

                return _keyId;
            }
        }

        public async Task<AuthenticateOut> AuthenticateAsync(AuthenticateIn args)
        {
            var appId = args.AppId;
            var challenge = CryptographicBuffer.ConvertStringToBinary(args.FinalChallenge, BinaryStringEncoding.Utf8);

            var keyCredentialRetrievalResult = await KeyCredentialManager.OpenAsync(appId);
            keyCredentialRetrievalResult.CheckStatus();

            var credential = keyCredentialRetrievalResult.Credential;
            var signedChallenge = await credential.SignAsync(challenge);
            var publicKey = credential.RetrievePublicKey();
            //var attestation = await credential.GetAttestationAsync();

            var authenticateOut = new AuthenticateOut();
            var builder = new AuthAssertionBuilder(this, publicKey, signedChallenge);

            authenticateOut.Assertion = await builder.GetAssertions();
            authenticateOut.AssertionScheme = AssertionScheme;

            return authenticateOut;
        }

        public IBuffer GetCertificate()
        {
            var cert = Convert.FromBase64String(AuthenticatoAttestationX509Certificate);
            return CryptographicBuffer.CreateFromByteArray(cert);
        }

        public IBuffer GetPublicKey()
        {
            var pub = Convert.FromBase64String(AuthenticatoAttestationPublicKey);
            return CryptographicBuffer.CreateFromByteArray(pub);
        }

        public async Task<RegisterOut> RegisterAsync(RegisterIn args)
        {
            var appId = args.AppId;
            var challenge = CryptographicBuffer.ConvertStringToBinary(args.FinalChallenge, BinaryStringEncoding.Utf8);

            // Create a new KeyCredential for the user on the device.
            var keyCredentialRetrievalResult = await KeyCredentialManager.RequestCreateAsync(appId, KeyCredentialCreationOption.ReplaceExisting);
            //keyCredentialRetrievalResult.CheckStatus();

            // User has autheniticated with Windows Hello and the key credential is created.
            var userKey = keyCredentialRetrievalResult.Credential;

            var publicKey = userKey.RetrievePublicKey();
            //var attestation = await userKey.GetAttestationAsync();

            var registerOut = new RegisterOut();
            var builder = new ReqAssertionBuilder(this, publicKey, challenge);

            registerOut.Assertion = await builder.GetAssertions();
            registerOut.AssertionScheme = AssertionScheme;

            return registerOut;
        }

        public async Task<IBuffer> SignAsync(IBuffer challenge)
        {
            var signer = SignerUtilities.GetSigner("SHA256WithECDSA");
            var key = PrivateKeyFactory.CreateKey(Convert.FromBase64String(AuthenticatoAttestationPrivateKey));
            signer.Init(true, key);
            var signature = signer.GenerateSignature();

            return await Task.Run(() => CryptographicBuffer.CreateFromByteArray(signature));
        }

        public AuthenticatorInfo GetAuthenticatorInfo()
        {
            return new AuthenticatorInfo
            {
                Aaid = "0000#0000",
                AuthenticatorIndex = 0,
                AuthenticationAlgorithm = 0x0, // Windows Hello using UAF_ALG_SIGN_RSASSA_PKCS1_SHA256_RAW
                AsmVersions = new Fido.Uaf.Shared.Messages.Version[] { Fido.Uaf.Shared.Messages.Version.GetVersion_1_0() },
                AssertionScheme = AssertionSchemes.UAFV1TLV,
                UserVerification = UserVerificationMethods.UserVerifyEyeprint | UserVerificationMethods.UserVerifyPasscode,
                KeyProtection = KeyProtectionTypes.KeyProtectionSoftware,
                MatcherProtection = MatcherProtectionTypes.MatcherProtectionSoftware,
                IsSecondFactorOnly = false,
                AttestationTypes = (short)TagTypes.TagAttestationBasicSurrogate,

                IsRoamingAuthenticator = false,
                HasSettings = false,
                TcDisplay = TransactionConfirmationDisplayTypes.TransactionConfirmationDisplayNone,
                Icon = ICON,
                Title = "Hello authenticator",
                Description = "UWP UAF Windows Hello authenticator",
            };
        }
    }
}
