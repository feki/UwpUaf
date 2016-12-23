using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpUaf.Client.Api;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

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
            fidoClientApi = new ClientApi();
        }

        void UwpUafAuthenticator_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UwpUafAuthenticator));
        }

        void SignatureTest_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WindowsHelloSignatureTest));
        }

        async void Button_Click(object sender, RoutedEventArgs e)
        {
            var discovery = await fidoClientApi.DiscoverAsync();
        }
    }
}
