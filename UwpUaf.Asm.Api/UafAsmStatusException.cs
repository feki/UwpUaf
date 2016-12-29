using System;
using Fido.Uaf.Shared.Messages.Asm;

namespace UwpUaf.Asm.Api
{
    public class UafAsmStatusException : Exception
    {
        public UafAsmStatusException(StatusCode statusCode) : base($"UafAsmStatusException Code={statusCode}")
        {
            StatusCode = statusCode;
        }

        public UafAsmStatusException(StatusCode statusCode, Exception innerException) : base($"UafAsmStatusException Code={statusCode}", innerException)
        {
            StatusCode = statusCode;
        }

        public StatusCode StatusCode { get; private set; }
    }
}
