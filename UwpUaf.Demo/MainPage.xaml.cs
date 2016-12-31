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

            DataContext = this;

            ClientApiSettings.UwpUafClientPackageFamilyName = "8453f20d-8176-488e-b710-2a95626b46b4_6m15y1xak46te";
            fidoClientApi = ClientApi.Instance;
        }

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

#pragma warning disable CC0057 // Unused parameters
        void AuthenticateTest_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(AuthenticateTest));

        async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            var discovery = await fidoClientApi.DiscoverAsync();
        }

        void DeregisterTest_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(DeregisterTest));

        void RegisterTest_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(RegisterTest));

        void SignatureTest_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(WindowsHelloSignatureTest));

        void UwpUafAuthenticator_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(UwpUafAuthenticator));
#pragma warning restore CC0057 // Unused parameters
    }
}
