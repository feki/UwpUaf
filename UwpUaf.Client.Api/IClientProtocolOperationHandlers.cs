using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Client.Api
{
    public interface IClientProtocolOperationHandlers
    {
        Task HandleRegistrationRequestAuthenticatorSelectionAsync(RegistrationRequest reg, AuthenticatorInfo[] availableAuthenticators, IOnAuthenticatorSelectedHandler handler);

        Task HandleAuthenticationRequestAuthenticatorSelectionAsync(AuthenticationRequest auth, AuthenticatorInfo[] registeredAuthenticators, IOnAuthenticatorSelectedHandler handler);

        Task HandleDeregistrationRequestConfirmationAsync(DeregistrationRequest dereg, IOnConfirmationHandler handler);
    }
}
