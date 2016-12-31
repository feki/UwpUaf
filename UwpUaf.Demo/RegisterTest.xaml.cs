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
    public sealed partial class RegisterTest : Page, INotifyPropertyChanged
    {
        public RegisterTest()
        {
            this.InitializeComponent();

            ClientApiSettings.UwpUafClientPackageFamilyName = "8453f20d-8176-488e-b710-2a95626b46b4_6m15y1xak46te";
            ClientApi = Client.Api.ClientApi.Instance;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string RegistrationError { get; set; }
        public string RegistrationRequestJson { get; set; } = "{\"header\":{\"upv\":{\"major\":1,\"minor\":0},\"op\":\"Reg\",\"appID\":\"8e1102fe-f044-49af-8385-876f3c6fd437_6m15y1xak46te\",\"serverData\":\"nwV8EPqS5raZdAgH3GD9Z-ytCA9MkiiWaCsr1GHHNJ2yUh3HaV1HHxd4Z67FefJOD5sQYZvipfg5BavhdWPMecD2SH39aJixoXN9ZaNwRlcftJe9WbtPNDC9q5V9WX7Z5jCwkAwehcI\"},\"challenge\":\"9pIcUwwrY5eD9o3OwfhkeHLnoIl0vaeJUbxSHMe_XgE\",\"username\":\"uwp_uaf_test\",\"policy\":{\"accepted\":[[{\"aaid\":[\"0000#0000\"]}],[{\"aaid\":[\"0000#0001\"]}]]}}";

        public string RegistrationResponseJson { get; set; }

        IClientApi ClientApi { get; set; }

#pragma warning disable CC0057 // Unused parameters
        async void ProcessRegisterRequest_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            RegistrationError = string.Empty;
            RegistrationResponseJson = string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegistrationError)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegistrationResponseJson)));

            var registrationRequest = JsonConvert.DeserializeObject<RegistrationRequest>(RegistrationRequestJson);
            var channelBinding = new ChannelBinding
            {
                CidPubkey = "",
                ServerEndPoint = "",
                TlsServerCertificate = "",
                TlsUnique = ""
            };
            try
            {
                var response = await ClientApi.RegisterAsync(registrationRequest, channelBinding);
                RegistrationResponseJson = JsonConvert.SerializeObject(response);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegistrationResponseJson)));
            }
            catch (Exception ex)
            {
                RegistrationError = $"{ex.Message}\n\n{ex.StackTrace}";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegistrationError)));
            }
        }
    }
}
