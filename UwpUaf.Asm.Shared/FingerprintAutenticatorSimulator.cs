using System;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.AuthenticatorCharacteristics;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Fido.Uaf.Shared.Tlv;
using Org.BouncyCastle.Security;
using UwpUaf.Asm.Shared.Op.Processor;
using UwpUaf.Shared;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Asm.Shared
{
    class FingerprintAutenticatorSimulator : IAuthenticator, IOnConfirmationHandler
    {
        const string AAID = "0000#0001";

        const string CERT_BASE64 = "MIICCjCCAbCgAwIBAgIgK7JjKpFsqc8+UCDr8ZGpCI+r+6DuiNwy9lEV0GmGxLMwCgYIKoZIzj0EAwIwfzELMAkGA1UEBhMCQ1oxFzAVBgNVBAgMDkN6ZWNoIFJlcHVibGljMQ0wCwYDVQQHDARCcm5vMRswGQYDVQQKDBJBSEVBRCBpVGVjLCBzLnIuby4xDTALBgNVBAsMBHNlbGYxHDAaBgNVBAMME3Rlc3RAYWhlYWQtaXRlYy5jb20wHhcNMTYxMjExMTk1NTI3WhcNMjYxMjExMTk1NTI3WjB/MQswCQYDVQQGEwJDWjEXMBUGA1UECAwOQ3plY2ggUmVwdWJsaWMxDTALBgNVBAcMBEJybm8xGzAZBgNVBAoMEkFIRUFEIGlUZWMsIHMuci5vLjENMAsGA1UECwwEc2VsZjEcMBoGA1UEAwwTdGVzdEBhaGVhZC1pdGVjLmNvbTBZMBMGByqGSM49AgEGCCqGSM49AwEHA0IABAStjiFc8ctJfbgAMyHGLU2acDz9vbSlzYPxYNZub9qG7pybq1/By2y6o+4rqDIzJan47pVS31K1ZC6HPqs2TgwwCgYIKoZIzj0EAwIDSAAwRQIhAKWnrz+iAm1oQaiW+L/ZncDwxiOAlVHpHbDBB13TR6q0AiAvzNJrgpEXcIfAz18q9hSMBSgu0LFnmTZVj/95ALrFow==";
        const string ICON = "data:image/png;base64,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";
        const string PRIVATE_KEY_BASE64 = "ME0CAQAwEwYHKoZIzj0CAQYIKoZIzj0DAQcEMzAxAgEBBCBs0AjkwtBjQmJkbr9E6k6cgFjBcnuRb85jvzHm2H9sSqAKBggqhkjOPQMBBw==";
        const string PUBLIC_KEY_BASE64 = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEBK2OIVzxy0l9uAAzIcYtTZpwPP29tKXNg/Fg1m5v2obunJurX8HLbLqj7iuoMjMlqfjulVLfUrVkLoc+qzZODA==";

        static FingerprintAutenticatorSimulator instance;
        TaskCompletionSource<bool> promise;

        FingerprintAutenticatorSimulator()
        {

        }

        public static FingerprintAutenticatorSimulator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FingerprintAutenticatorSimulator();
                }

                return instance;
            }
        }

        public string Aaid => "0000#0001";

        public string AssertionScheme => AssertionSchemes.UAFV1TLV;

        public Frame Frame { get; set; }

        public string KeyId
        {
            get
            {
                var rs = new StringBuilder();
                var rnd = new byte[16];
                var random = new SecureRandom();

                random.NextBytes(rnd);
                rs.Append("$2a$10$");
                rs.Append(Convert.ToBase64String(rnd));

                var keyId = "UwpUaf-FingerprintAutenticatorSimulator-Key-" + Convert.ToBase64String(Encoding.UTF8.GetBytes(rs.ToString()));

                return keyId.ConvertToBase64UrlString();
            }
        }

        public async Task<AuthenticateOut> AuthenticateAsync(AuthenticateIn authenticateIn)
        {
            var appId = authenticateIn.AppId;
            var challenge = CryptographicBuffer.ConvertStringToBinary(authenticateIn.FinalChallenge, BinaryStringEncoding.Utf8);

            promise = new TaskCompletionSource<bool>();

            var parameter = new Ui.AuthenticationConfirmationParameter
            {
                AuthenticatorInfo = GetAuthenticatorInfo(),
                AuthenticateIn = authenticateIn,
                ConfirmationHandler = this
            };
            Frame.Navigate(typeof(Ui.FingerprintAuthenticatorSimulatorAuthenticationConfirmation), parameter);

            await promise.Task;

            var authenticateOut = new AuthenticateOut();
            var builder = new AuthAssertionBuilder(this, challenge);

            authenticateOut.Assertion = await builder.GetAssertionsAsync();
            authenticateOut.AssertionScheme = AssertionScheme;

            return authenticateOut;
        }

        public async Task<bool> DeregisterAsync(DeregisterIn args)
        {
            var appId = args.AppId;

            promise = new TaskCompletionSource<bool>();

            var parameter = new Ui.DeregistrationConfirmationParameter
            {
                AuthenticatorInfo = GetAuthenticatorInfo(),
                DeregisterIn = args,
                ConfirmationHandler = this
            };
            Frame.Navigate(typeof(Ui.DeregistrationConfirmation), parameter);

            return await promise.Task;
        }

        public AuthenticatorInfo GetAuthenticatorInfo()
        {
            return new AuthenticatorInfo
            {
                Aaid = AAID,
                AuthenticatorIndex = 1,
                AuthenticationAlgorithm = AuthenticationAlgorithms.UafAlgSignSecp256r1EcdsaSha256Raw,
                AsmVersions = new Fido.Uaf.Shared.Messages.Version[] { Fido.Uaf.Shared.Messages.Version.GetVersion_1_0() },
                AssertionScheme = AssertionSchemes.UAFV1TLV,
                UserVerification = UserVerificationMethods.UserVerifyFingerprint,
                KeyProtection = KeyProtectionTypes.KeyProtectionSoftware,
                MatcherProtection = MatcherProtectionTypes.MatcherProtectionSoftware,
                IsSecondFactorOnly = false,
                AttachmentHint = AttachmentHints.AttachmentHintInternal,
                AttestationTypes = (short)TagTypes.TagAttestationBasicFull,

                Icon = ICON,
                IsRoamingAuthenticator = false,
                HasSettings = false,
                TcDisplay = TransactionConfirmationDisplayTypes.TransactionConfirmationDisplayAny,
                Description = "UWP UAF Fingerprint authenticator simulator",
                Title = "Fingerprint simulator"
            };
        }

        public IBuffer GetCertificate()
        {
            var cert = Convert.FromBase64String(CERT_BASE64);
            var buffer = CryptographicBuffer.CreateFromByteArray(cert);

            return buffer;
        }

        public IBuffer GetPublicKey()
        {
            var pub = Convert.FromBase64String(PUBLIC_KEY_BASE64);
            var buffer = CryptographicBuffer.CreateFromByteArray(pub);

            return buffer;
        }

        public async Task OnCancelationAsync(StatusCode statusCode = StatusCode.UafAsmStatusUserCancelled)
        {
            await Task.Delay(0);
            promise?.TrySetException(new AsmStatusCodeException(statusCode));
        }

        public async Task OnConfirmationAsync()
        {
            await Task.Delay(0);
            promise?.TrySetResult(true);
        }

        public async Task<RegisterOut> RegisterAsync(RegisterIn registerIn)
        {
            var appId = registerIn.AppId;
            var challenge = CryptographicBuffer.ConvertStringToBinary(registerIn.FinalChallenge, BinaryStringEncoding.Utf8);

            promise = new TaskCompletionSource<bool>();

            var parameter = new Ui.RegistrationConfirmationParameter
            {
                AuthenticatorInfo = GetAuthenticatorInfo(),
                RegisterIn = registerIn,
                ConfirmationHandler = this
            };
            Frame.Navigate(typeof(Ui.FingerprintAuthenticatorSimulatorRegistrationConfirmation), parameter);

            await promise.Task;

            var registerOut = new RegisterOut();
            var builder = new ReqAssertionBuilder(this, GetPublicKey(), challenge);

            registerOut.Assertion = await builder.GetAssertionsAsync((TagTypes)registerIn.AttestationType);
            registerOut.AssertionScheme = AssertionScheme;

            return registerOut;
        }

        public async Task<IBuffer> SignAsync(IBuffer challenge)
        {
            var signer = SignerUtilities.GetSigner("SHA256WithECDSA");
            var key = PrivateKeyFactory.CreateKey(Convert.FromBase64String(PRIVATE_KEY_BASE64));
            signer.Init(true, key);
            var signature = signer.GenerateSignature();

            return await Task.Run(() => CryptographicBuffer.CreateFromByteArray(signature));
        }
    }
}
