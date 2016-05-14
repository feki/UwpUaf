using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpUaf.Authenticator;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpUaf.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UwpUafAuthenticator : Page
    {
        private IUwpUafAuthenticator uwpUafAuthenticator;

        public UwpUafAuthenticator()
        {
            this.InitializeComponent();

            this.uwpUafAuthenticator = new Authenticator.UwpUafAuthenticator();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var appId = this.AppId.Text;
            var challenge = CryptographicBuffer.ConvertStringToBinary(this.Challenge.Text, BinaryStringEncoding.Utf8);

            var registerResponse = await this.uwpUafAuthenticator.RegisterAsync(appId, challenge);

            this.SignedChallenge.Text = CryptographicBuffer.EncodeToBase64String(registerResponse.SignedChallenge);
            this.PublicKey.Text = CryptographicBuffer.EncodeToBase64String(registerResponse.PublicKey);
        }

        private async void SignButton_Click(object sender, RoutedEventArgs e)
        {
            var appId = this.AppId.Text;
            var challenge = CryptographicBuffer.ConvertStringToBinary(this.Challenge.Text, BinaryStringEncoding.Utf8);

            var signResponse = await this.uwpUafAuthenticator.SignAsync(appId, challenge);

            this.SignedChallenge.Text = CryptographicBuffer.EncodeToBase64String(signResponse.SignedChallenge);
        }

        private void UnregisterButton_Click(object sender, RoutedEventArgs e)
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
    }
}
