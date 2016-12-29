using System;
using System.ComponentModel;
using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;
using UwpUaf.Client.Api;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Demo
{
    public sealed partial class DeregisterTest : Page, INotifyPropertyChanged
    {
        public DeregisterTest()
        {
            InitializeComponent();

            ClientApiSettings.UwpUafClientPackageFamilyName = "8453f20d-8176-488e-b710-2a95626b46b4_6m15y1xak46te";
            ClientApi = Client.Api.ClientApi.Instance;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string DeregistrationError { get; set; }
        public string DeregistrationRequestJson { get; set; } = "{\"header\":{\"upv\":{\"major\":1,\"minor\":0},\"op\":\"Dereg\",\"appID\":\"8e1102fe-f044-49af-8385-876f3c6fd437_6m15y1xak46te\"},\"authenticators\":[{\"aaid\":\"0000#0000\",\"keyID\":\"key1\"},{\"aaid\":\"0000#0001\",\"keyID\":\"key2\"}]}";

        public string DeregistrationResponseJson { get; set; }

        IClientApi ClientApi { get; set; }

#pragma warning disable CC0057 // Unused parameters
        async void ProcessDeregisterRequest_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            DeregistrationError = string.Empty;
            DeregistrationResponseJson = string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeregistrationError)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeregistrationResponseJson)));

            var deregistrationRequest = JsonConvert.DeserializeObject<DeregistrationRequest>(DeregistrationRequestJson);
            var channelBinding = new ChannelBinding
            {
                CidPubkey = "",
                ServerEndPoint = "",
                TlsServerCertificate = "",
                TlsUnique = ""
            };
            try
            {
                await ClientApi.DeregisterAsync(deregistrationRequest, channelBinding);
                DeregistrationResponseJson = "Deregistered";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeregistrationResponseJson)));
            }
            catch (Exception ex)
            {
                DeregistrationError = $"{ex.Message}\n\n{ex.StackTrace}";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeregistrationError)));
            }
        }
    }
}
