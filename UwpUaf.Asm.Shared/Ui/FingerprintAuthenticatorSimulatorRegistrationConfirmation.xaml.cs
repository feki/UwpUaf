using System.ComponentModel;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpUaf.Asm.Shared.Ui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FingerprintAuthenticatorSimulatorRegistrationConfirmation : Page, INotifyPropertyChanged
    {
        IOnConfirmationHandler handler;

        public FingerprintAuthenticatorSimulatorRegistrationConfirmation()
        {
            this.InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AuthenticatorInfo AuthenticatorInfo { get; private set; }

        public RegisterIn RegisterIn { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameter = e.Parameter as RegistrationConfirmationParameter;
            AuthenticatorInfo = parameter.AuthenticatorInfo;
            RegisterIn = parameter.RegisterIn;
            handler = parameter.ConfirmationHandler;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegisterIn)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthenticatorInfo)));
        }

#pragma warning disable CC0057 // Unused parameters
        async void Confirm_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            if (handler != null)
            {
                await handler.OnConfirmationAsync();
            }
        }

#pragma warning disable CC0057 // Unused parameters
        async void Cancel_ClickAsync(object sender, RoutedEventArgs e)
#pragma warning restore CC0057 // Unused parameters
        {
            await handler.OnCancelationAsync();
        }
    }
}
