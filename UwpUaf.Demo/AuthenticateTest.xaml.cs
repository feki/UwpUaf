using System;
using System.ComponentModel;
using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;
using UwpUaf.Client.Api;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthenticateTest : Page, INotifyPropertyChanged
    {
        public AuthenticateTest()
        {
            InitializeComponent();

            ClientApiSettings.UwpUafClientPackageFamilyName = "8453f20d-8176-488e-b710-2a95626b46b4_6m15y1xak46te";
            ClientApi = Client.Api.ClientApi.Instance;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string AuthenticationError { get; set; }
        public string AuthenticationRequestJson { get; set; } = "{\"header\":{\"upv\":{\"major\":1,\"minor\":0},\"op\":\"Auth\",\"appID\":\"8e1102fe-f044-49af-8385-876f3c6fd437_6m15y1xak46te\",\"serverData\":\"emKubKMS8RxYOth7J8enT_x7dQWBaO1CiC0fGmSEhX56kq2RYo1LRpwvfHlzYRI3p9Ay-l4zJcV3lX6rQ0CYNWi5nNDabClFm3k0pPj0kX5V-db9ejN_05y2J6wqztSD\"},\"challenge\":\"1AM2yZY4-9SG4Ns7-hMdB8IV_FTDKFFiUqNJNVbsVoo\",\"policy\":{\"accepted\":[[{\"aaid\":[\"0000#0000\"]}],[{\"aaid\":[\"0000#0001\"]}]]}}";

        public string AuthenticationResponseJson { get; set; }

        IClientApi ClientApi { get; set; }

#pragma warning disable CC0057 // Unused parameters
        async void ProcessAuthenticationRequest_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            AuthenticationError = string.Empty;
            AuthenticationResponseJson = string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthenticationError)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthenticationResponseJson)));

            var authenticationRequest = JsonConvert.DeserializeObject<AuthenticationRequest>(AuthenticationRequestJson);
            var channelBinding = new ChannelBinding
            {
                CidPubkey = "",
                ServerEndPoint = "",
                TlsServerCertificate = "",
                TlsUnique = ""
            };
            try
            {
                var response = await ClientApi.AuthenticateAsync(authenticationRequest, channelBinding);
                AuthenticationResponseJson = JsonConvert.SerializeObject(response);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthenticationResponseJson)));
            }
            catch (Exception ex)
            {
                AuthenticationError = $"{ex.Message}\n\n{ex.StackTrace}";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthenticationError)));
            }
        }
    }
}
