using System;

namespace UwpUaf.Client.Api
{
    internal class FidoOperationErrorCodeException : Exception
    {
        public FidoOperationErrorCodeException()
        {
        }

        public FidoOperationErrorCodeException(string message) : base(message)
        {
        }

        public FidoOperationErrorCodeException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public FidoOperationErrorCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ErrorCode ErrorCode { get; private set; }
    }
}