using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Asm.Shared.Ui
{
    class AuthenticationConfirmationParameter
    {
        public AuthenticateIn AuthenticateIn { get; set; }

        public AuthenticatorInfo AuthenticatorInfo { get; set; }

        public IOnConfirmationHandler ConfirmationHandler { get; set; }
    }
}
