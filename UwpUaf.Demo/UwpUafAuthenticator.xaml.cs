using System;
using UwpUaf.Authenticator;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpUaf.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UwpUafAuthenticator : Page
    {
        IUwpUafAuthenticator uwpUafAuthenticator;

        public UwpUafAuthenticator()
        {
            this.InitializeComponent();

            this.uwpUafAuthenticator = new Authenticator.UwpUafAuthenticator();
        }

        async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var appId = this.AppId.Text;
            var challenge = CryptographicBuffer.ConvertStringToBinary(this.Challenge.Text, BinaryStringEncoding.Utf8);

            var registerResponse = await this.uwpUafAuthenticator.RegisterAsync(appId, challenge);

            this.SignedChallenge.Text = CryptographicBuffer.EncodeToBase64String(registerResponse.SignedChallenge);
            this.PublicKey.Text = CryptographicBuffer.EncodeToBase64String(registerResponse.PublicKey);
        }

        async void SignButton_Click(object sender, RoutedEventArgs e)
        {
            var appId = this.AppId.Text;
            var challenge = CryptographicBuffer.ConvertStringToBinary(this.Challenge.Text, BinaryStringEncoding.Utf8);

            var signResponse = await this.uwpUafAuthenticator.SignAsync(appId, challenge);

            SignedChallenge.Text = CryptographicBuffer.EncodeToBase64String(signResponse.SignedChallenge);
            if (PublicKey.Text.Length == 0)
            {
                PublicKey.Text = CryptographicBuffer.EncodeToBase64String(signResponse.PublicKey);
            }
            if (Attestation.Text.Length == 0)
            {
                byte[] encryptedbyteArr;
                CryptographicBuffer.CopyToByteArray(signResponse.Attestation, out encryptedbyteArr);
                Attestation.Text = System.Text.Encoding.UTF8.GetString(encryptedbyteArr);
            }

            VerifySignature();
        }

        void UnregisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var appId = this.AppId.Text;
                this.uwpUafAuthenticator.UnregisterAsync(appId);
                this.UnregisterResult.Text = true.ToString();
            }
            catch (Exception)
            {
                this.UnregisterResult.Text = false.ToString();
            }
        }

        void VerifySignature()
        {
            // the original data, pulled from the textbox on screen
            var challenge = CryptographicBuffer.ConvertStringToBinary(Challenge.Text, BinaryStringEncoding.Utf8);
            // the signature data, pulled from the textbox on screen
            var signedChallange = CryptographicBuffer.DecodeFromBase64String(SignedChallenge.Text);
            // the public key, pulled from the textbox on screen
            var publicKey = CryptographicBuffer.DecodeFromBase64String(PublicKey.Text);

            // not 100% sure that I have this algorithm right, needs more checking.
            var algorithm = AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithmNames.RsaSignPkcs1Sha256);

            var cryptoKey = algorithm.ImportPublicKey(publicKey);
            var verified = CryptographicEngine.VerifySignature(cryptoKey, challenge, signedChallange);

            // fire and forget...
            SignatureVerificationResult.Text = $"The signature verification result was {verified}";
        }
    }
}
