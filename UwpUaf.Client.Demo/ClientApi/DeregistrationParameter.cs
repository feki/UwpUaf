using Fido.Uaf.Shared.Messages;
using UwpUaf.Client.Api;

namespace UwpUaf.Client.Demo.ClientApi
{
    class DeregistrationParameter
    {
        public DeregistrationParameter(DeregistrationRequest dereg, IOnConfirmationHandler handler)
        {
            Dereg = dereg;
            Handler = handler;
        }

        public DeregistrationRequest Dereg { get; private set; }

        public IOnConfirmationHandler Handler { get; private set; }
    }
}