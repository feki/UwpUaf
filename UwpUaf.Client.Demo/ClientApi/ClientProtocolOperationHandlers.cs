using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Client.Api;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Client.Demo.ClientApi
{
    class ClientProtocolOperationHandlers : IClientProtocolOperationHandlers
    {
        private readonly Frame frame;

        public ClientProtocolOperationHandlers(Frame frame)
        {
            this.frame = frame;
        }

        public Task HandleAuthenticationRequestAuthenticatorSelectionAsync(AuthenticationRequest auth, AuthenticatorInfo[] registeredAuthenticators, IOnAuthenticatorSelectedHandler handler)
        {
            throw new NotImplementedException();
        }

        public Task HandleDeregistrationRequestConfirmationAsync(DeregistrationRequest dereg, IOnConfirmationHandler handler)
        {
            throw new NotImplementedException();
        }

        public async Task HandleRegistrationRequestAuthenticatorSelectionAsync(RegistrationRequest reg, AuthenticatorInfo[] availableAuthenticators, IOnAuthenticatorSelectedHandler handler)
        {
            await Task.Delay(0);
            var parameter = new RegistrationParams(reg, availableAuthenticators, handler);
            frame.Navigate(typeof(RegisterUafAuthenticaror), parameter);
        }
    }
}
