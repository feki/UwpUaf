using System;
using System.Linq;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Client.Api.Operations
{
    class AuthOperation : OperationBase, IOnAuthenticatorSelectedHandler
    {

        TaskCompletionSource<OperationResponseBase> promise;

        public AuthOperation(AuthenticationRequest authenticationRequest) : base(authenticationRequest)
        {
        }

        AuthenticationRequest AuthenticationRequest
        {
            get
            {
                return operation as AuthenticationRequest;
            }
        }

        public async Task OnAuthenticatorSelectedAsync(AuthenticatorInfo authenticatorInfo)
        {
            var fcp = CreateFinalChallengeParams(AuthenticationRequest, ChannelBinding, CallerPackageFamilyName);
            var auth = new AuthenticateIn
            {
                AppId = AuthenticationRequest.Header.AppId,
                FinalChallenge = fcp
            };

            var response = await asmApi.AuthenticateAsync(auth, clientApi.authenticatorIdToPackageFamilyName[authenticatorInfo.Aaid], (ushort)authenticatorInfo.AuthenticatorIndex);

            var authenticationResponse = new AuthenticationResponse
            {
                Header = AuthenticationRequest.Header,
                FcParams = fcp,
                Assertions = new AuthenticatorSignAssertion[]
                {
                    new AuthenticatorSignAssertion { Assertion = response.Assertion, AssertionScheme = response.AssertionScheme }
                }
            };

            promise.TrySetResult(authenticationResponse);
        }

        public async Task OnCancelationAsync(ErrorCode errorCode = ErrorCode.UserCancelled)
        {
            await Task.Delay(0);
            promise.TrySetException(new FidoOperationErrorCodeException(errorCode));
        }

        public async override Task<OperationResponseBase> ProcessOperationAsync()
        {
            var facetId = CallerPackageFamilyName;
            var checkFacetId = await CheckTrustFacetIdAsync(AuthenticationRequest.Header.AppId, facetId);
            if (!checkFacetId)
            {
                throw new FidoOperationErrorCodeException(ErrorCode.UntrustedFacetId);
            }

            var authenticators = await clientApi.GetAvailableAuthenticatorsAsync();
            //var registeredAuthenticators = authenticators.Where(a => a.IsUserEnrolled).ToArray();
            var registeredAuthenticators = authenticators.ToArray();

            promise = new TaskCompletionSource<OperationResponseBase>();
            // 4.Filter available authenticators with the given policy and present the filtered list to User.
            //   If AuthenticationRequest.policy.accepted list is empty then suggest any registered authenticator to the user for authentication
            // 5.Let the user select the preferred Authenticator.
            await Handlers.HandleAuthenticationRequestAuthenticatorSelectionAsync(AuthenticationRequest, registeredAuthenticators, this);

            return await promise.Task;
        }
    }
}
