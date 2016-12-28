using UwpUaf.Client.Api;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly ClientApi fidoClientApi;

        public MainPage()
        {
            this.InitializeComponent();

            ClientApiSettings.UwpUafClientPackageFamilyName = "8453f20d-8176-488e-b710-2a95626b46b4_6m15y1xak46te";
            fidoClientApi = ClientApi.Instance;
        }

#pragma warning disable CC0057 // Unused parameters
        async void Button_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            var discovery = await fidoClientApi.DiscoverAsync();
        }

#pragma warning disable CC0057 // Unused parameters
        private void RegisterTest_Click(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            Frame.Navigate(typeof(RegisterTest));
        }

#pragma warning disable CC0057 // Unused parameters
        void SignatureTest_Click(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            Frame.Navigate(typeof(WindowsHelloSignatureTest));
        }

#pragma warning disable CC0057 // Unused parameters
        void UwpUafAuthenticator_Click(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            Frame.Navigate(typeof(UwpUafAuthenticator));
        }
    }
}
