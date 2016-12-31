using System.Collections.ObjectModel;
using System.Linq;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Client.Demo.ClientApi;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpUaf.Client.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeregisterUafAuthenticator : Page
    {
        readonly Api.ClientApi clientApi;
        DeregistrationParameter deregistrationParams;

        public DeregisterUafAuthenticator()
        {
            InitializeComponent();

            clientApi = Api.ClientApi.Instance;
            DataContext = this;
        }

        public ObservableCollection<AuthenticatorInfo> DeregisterAuthenticators { get; set; } = new ObservableCollection<AuthenticatorInfo>();

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is DeregistrationParameter)
            {
                deregistrationParams = e.Parameter as DeregistrationParameter;

                var aaids = deregistrationParams.Dereg.Authenticators.Select(a => a.Aaid);
                var availableAuthenticators = await clientApi.GetAvailableAuthenticatorsAsync();
                var deregisterAuthenticators = availableAuthenticators.Where(a => aaids.Contains(a.Aaid));

                foreach (var auth in deregisterAuthenticators)
                {
                    DeregisterAuthenticators.Add(auth);
                }
            }
        }

#pragma warning disable CC0057 // Unused parameters
        async void Cancel_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            await deregistrationParams.Handler.OnCancelationAsync();
        }

#pragma warning disable CC0057 // Unused parameters
        async void Confirm_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            await deregistrationParams.Handler.OnConfirmationAsync();
        }
    }
}
