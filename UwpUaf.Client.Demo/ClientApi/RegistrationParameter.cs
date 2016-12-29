using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Client.Api;

namespace UwpUaf.Client.Demo.ClientApi
{
    class RegistrationParameter
    {

        public RegistrationRequest Reg { get; private set; }

        public AuthenticatorInfo[] AvailableAuthenticators { get; private set; }

        public IOnAuthenticatorSelectedHandler Handler { get; private set; }

        public RegistrationParameter(RegistrationRequest reg, AuthenticatorInfo[] availableAuthenticators, IOnAuthenticatorSelectedHandler handler)
        {
            this.Reg = reg;
            this.AvailableAuthenticators = availableAuthenticators;
            this.Handler = handler;
        }
    }
}
