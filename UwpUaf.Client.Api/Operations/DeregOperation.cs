using System;
using System.Linq;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Client.Api.Operations
{
    class DeregOperation : OperationBase, IOnConfirmationHandler
    {
        TaskCompletionSource<OperationResponseBase> promise;

        DeregistrationRequest DeregistrationRequest
        {
            get
            {
                return operation as DeregistrationRequest;
            }
        }

        public DeregOperation(DeregistrationRequest deregistrationRequest) : base(deregistrationRequest)
        {
        }

        public async override Task<OperationResponseBase> ProcessOperationAsync()
        {
            promise = new TaskCompletionSource<OperationResponseBase>();
            await Handlers.HandleDeregistrationRequestConfirmationAsync(DeregistrationRequest, this);

            return await promise.Task;
        }

        public async Task OnConfirmationAsync()
        {
            var auths = await clientApi.GetAvailableAuthenticatorsAsync();
            foreach (var auth in DeregistrationRequest.Authenticators)
            {
                var deregisterIn = new DeregisterIn
                {
                    AppId = DeregistrationRequest.Header.AppId,
                    KeyId = auth.KeyId
                };
                var asmPackageFamilyName = clientApi.authenticatorIdToPackageFamilyName[auth.Aaid];
                var authInfo = auths.First(a => a.Aaid == auth.Aaid);
                await asmApi.DeregisterAsync(deregisterIn, asmPackageFamilyName, (ushort)authInfo.AuthenticatorIndex);
            }

            promise.TrySetResult(null);
        }

        public async Task OnCancelationAsync(ErrorCode errorCode = ErrorCode.UserCancelled)
        {
            await Task.Delay(0);
            promise.TrySetException(new FidoOperationErrorCodeException(errorCode));
        }
    }
}
