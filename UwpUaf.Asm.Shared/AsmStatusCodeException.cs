using System;
using Fido.Uaf.Shared.Messages.Asm;

namespace UwpUaf.Asm.Shared
{
    internal class AsmStatusCodeException : Exception
    {
        readonly StatusCode statusCode;

        public AsmStatusCodeException()
        {
        }

        public AsmStatusCodeException(string message) : base(message)
        {
        }

        public AsmStatusCodeException(StatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public AsmStatusCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public StatusCode StatusCode
        {
            get
            {
                return statusCode;
            }
        }
    }
}