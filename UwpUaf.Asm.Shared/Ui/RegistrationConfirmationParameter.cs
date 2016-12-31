using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Asm.Shared.Ui
{
    class RegistrationConfirmationParameter
    {

        public AuthenticatorInfo AuthenticatorInfo { get; set; }

        public IOnConfirmationHandler ConfirmationHandler { get; set; }
        public RegisterIn RegisterIn { get; set; }
    }
}
