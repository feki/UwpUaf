using System.Collections.ObjectModel;
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
    public sealed partial class RegisterUafAuthenticator : Page
    {
        readonly ObservableCollection<AuthenticatorInfo> authenticatorInfoItems = new ObservableCollection<AuthenticatorInfo>();
        readonly Api.IClientApi clientApi;
        RegistrationParameter registrationParams;

        public RegisterUafAuthenticator()
        {
            InitializeComponent();

            clientApi = Api.ClientApi.Instance;
            DataContext = this;
        }

        public ObservableCollection<AuthenticatorInfo> AvailableAuthenticators
        {
            get
            {
                return authenticatorInfoItems;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is RegistrationParameter)
            {
                registrationParams = e.Parameter as RegistrationParameter;
                foreach (var auth in registrationParams.AvailableAuthenticators)
                {
                    authenticatorInfoItems.Add(auth);
                }
            }
            else
            {
                var availableAuthenticators = await clientApi.GetAvailableAuthenticatorsAsync();
                foreach (var auth in availableAuthenticators)
                {
                    authenticatorInfoItems.Add(auth);
                }
            }
        }

#pragma warning disable CC0057 // Unused parameters
        async void AvailableAuthenticatorList_ItemClickAsync(object sender, ItemClickEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            var authenticator = e.ClickedItem as AuthenticatorInfo;
            await registrationParams.Handler.OnAuthenticatorSelectedAsync(authenticator);
        }

#pragma warning disable CC0057 // Unused parameters
        async void Cancel_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            await registrationParams.Handler.OnCancelationAsync();
        }
    }
}
