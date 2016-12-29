using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Asm.Shared.Ui
{
    class DeregistrationConfirmationParameter
    {
        public AuthenticatorInfo AuthenticatorInfo { get; set; }

        public IOnConfirmationHandler ConfirmationHandler { get; set; }

        public DeregisterIn DeregisterIn { get; set; }
    }
}