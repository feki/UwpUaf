using System;
using Fido.Uaf.Shared.Messages.Asm;

namespace UwpUaf.Asm.Api
{
    public class UafAsmStatusException : Exception
    {
        public UafAsmStatusException(StatusCode code) : base()
        {
            Code = code;
        }

        public StatusCode Code { get; private set; }
    }
}
