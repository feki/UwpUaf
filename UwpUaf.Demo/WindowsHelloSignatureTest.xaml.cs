using System;
using System.Collections.Generic;
using UwpUaf.Authenticator;
using Windows.Security.Cryptography;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;
using System.IO;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpUaf.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WindowsHelloSignatureTest : Page
    {
        string shareText = "";
        IUwpUafAuthenticator uwpUafAuthenticator;

        public WindowsHelloSignatureTest()
        {
            this.InitializeComponent();

            this.uwpUafAuthenticator = new Authenticator.UwpUafAuthenticator();

            var dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        bool IsSupported { get; set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            IsSupported = await uwpUafAuthenticator.IsSupportedAsync();
            DataContext = this;
            base.OnNavigatedTo(e);
        }

        void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            request.Data.Properties.Title = "Share Text Example";
            request.Data.Properties.Description = "An example of how to share text.";
            request.Data.SetData(StandardDataFormats.Text, shareText);
        }

        async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var appId = this.AppId.Text;
            var challenge = CryptographicBuffer.ConvertStringToBinary(this.Challenge.Text, BinaryStringEncoding.Utf8);

            await uwpUafAuthenticator.RegisterAsync(appId, challenge);
        }

        async void SignButton_Click(object sender, RoutedEventArgs e)
        {
            var appId = this.AppId.Text;
            var challenge = CryptographicBuffer.ConvertStringToBinary(this.Challenge.Text, BinaryStringEncoding.Utf8);

            var signResponses = new List<SignResponse>();
            foreach (var i in Convert.ToInt32(NumberOfCreation.Text).Times())
            {
                var signResponse = await uwpUafAuthenticator.SignAsync(appId, challenge);
                signResponses.Add(signResponse);
            }

            using (var sw = new StringWriter())
            {
                await sw.WriteLineAsync("Attestation,PublicKey,SignedChallenge");
                foreach (var sign in signResponses)
                {
                    var objs = new object[]
                    {
                        CryptographicBuffer.EncodeToBase64String(sign.Attestation),
                        CryptographicBuffer.EncodeToBase64String(sign.PublicKey),
                        CryptographicBuffer.EncodeToBase64String(sign.SignedChallenge)
                    };
                    sw.WriteLine("{0},{1},{2}", objs);
                }
                shareText = sw.ToString();
            }

            DataTransferManager.ShowShareUI();

            //var signResponse = await this.uwpUafAuthenticator.SignAsync(appId, challenge);
            //
            //SignedChallenge.Text = CryptographicBuffer.EncodeToBase64String(signResponse.SignedChallenge);
            //if (PublicKey.Text.Length == 0)
            //{
            //    PublicKey.Text = CryptographicBuffer.EncodeToBase64String(signResponse.PublicKey);
            //}
            //if (Attestation.Text.Length == 0)
            //{
            //    byte[] encryptedbyteArr;
            //    CryptographicBuffer.CopyToByteArray(signResponse.Attestarion, out encryptedbyteArr);
            //    Attestation.Text = System.Text.Encoding.UTF8.GetString(encryptedbyteArr);
            //}
            //
            //VerifySignature();
        }

        void UnregisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var appId = this.AppId.Text;
                uwpUafAuthenticator.UnregisterAsync(appId);
                UnregisterResult.Text = true.ToString();
            }
            catch (Exception)
            {
                UnregisterResult.Text = false.ToString();
            }
        }
    }
}
