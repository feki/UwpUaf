using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Asm.Shared.Ui
{
    class RegistrationConfirmationParameter
    {
        public RegisterIn RegisterIn { get; set; }

        public AuthenticatorInfo AuthenticatorInfo { get; set; }

        public IOnConfirmationHandler ConfirmationHandler { get; set; }
    }
}
