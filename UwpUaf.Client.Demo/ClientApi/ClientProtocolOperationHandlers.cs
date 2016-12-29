using System;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Client.Api;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Client.Demo.ClientApi
{
    class ClientProtocolOperationHandlers : IClientProtocolOperationHandlers
    {
        readonly Frame frame;

        public ClientProtocolOperationHandlers(Frame frame)
        {
            this.frame = frame;
        }

        public async Task HandleAuthenticationRequestAuthenticatorSelectionAsync(AuthenticationRequest auth, AuthenticatorInfo[] registeredAuthenticators, IOnAuthenticatorSelectedHandler handler)
        {
            await Task.Delay(0);
            var parameter = new AuthenticationParameter(auth, registeredAuthenticators, handler);
            frame.Navigate(typeof(AuthenticateUafAuthenticator), parameter);
        }

        public async Task HandleDeregistrationRequestConfirmationAsync(DeregistrationRequest dereg, IOnConfirmationHandler handler)
        {
            await Task.Delay(0);
            var parameter = new DeregistrationParameter(dereg, handler);
            frame.Navigate(typeof(DeregisterUafAuthenticator), parameter);
        }

        public async Task HandleRegistrationRequestAuthenticatorSelectionAsync(RegistrationRequest reg, AuthenticatorInfo[] availableAuthenticators, IOnAuthenticatorSelectedHandler handler)
        {
            await Task.Delay(0);
            var parameter = new RegistrationParameter(reg, availableAuthenticators, handler);
            frame.Navigate(typeof(RegisterUafAuthenticator), parameter);
        }
    }
}
