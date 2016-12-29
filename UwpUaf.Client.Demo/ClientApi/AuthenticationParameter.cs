using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Client.Api;

namespace UwpUaf.Client.Demo.ClientApi
{
    class AuthenticationParameter
    {
        public AuthenticationParameter(AuthenticationRequest auth, AuthenticatorInfo[] registeredAuthenticators, IOnAuthenticatorSelectedHandler handler)
        {
            Auth = auth;
            RegisteredAuthenticators = registeredAuthenticators;
            Handler = handler;
        }

        public AuthenticationRequest Auth { get; private set; }

        public IOnAuthenticatorSelectedHandler Handler { get; private set; }

        public AuthenticatorInfo[] RegisteredAuthenticators { get; private set; }
    }
}