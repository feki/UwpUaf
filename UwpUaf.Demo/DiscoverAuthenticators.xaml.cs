using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpUaf.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiscoverAuthenticators : Page
    {
        private readonly IClientApi uwpUafClientApi;

        public DiscoverAuthenticators()
        {
            this.InitializeComponent();

            uwpUafClientApi = new ClientApi();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var discoveryData = await uwpUafClientApi.DiscoverAsync();
            DiscoveryDataJson.Text = JsonConvert.SerializeObject(discoveryData, Formatting.Indented);

            base.OnNavigatedTo(e);
        }
    }
}
