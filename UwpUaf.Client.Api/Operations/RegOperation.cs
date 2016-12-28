using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Client.Api.Operations
{
    class RegOperation : OperationBase, IOnAuthenticatorSelectedHandler
    {
        TaskCompletionSource<OperationResponseBase> promise;

        RegistrationRequest RegistrationRequest
        {
            get
            {
                return operation as RegistrationRequest;
            }
        }

        public RegOperation(RegistrationRequest registrationRequest) : base(registrationRequest)
        {
        }

        public async override Task<OperationResponseBase> ProcessOperationAsync()
        {
            var facetId = CallerPackageFamilyName;
            var checkFacetId = await CheckTrustFacetIdAsync(RegistrationRequest.Header.AppId, facetId);
            if (!checkFacetId)
            {
                throw new FidoOperationErrorCodeException(ErrorCode.UntrustedFacetId);
            }

            var authenticators = await clientApi.GetAvailableAuthenticators();
            var availableAuthenticators = authenticators.Where(a => !a.IsUserEnrolled).ToArray();

            promise = new TaskCompletionSource<OperationResponseBase>();
            // 4.Filter available authenticators with the given policy and present the filtered list to User.
            //   If AuthenticationRequest.policy.accepted list is empty then suggest any registered authenticator to the user for authentication
            // 5.Let the user select the preferred Authenticator.
            await Handlers.HandleRegistrationRequestAuthenticatorSelectionAsync(RegistrationRequest, availableAuthenticators, this);

            return await promise.Task;
        }

        public async Task OnAuthenticatorSelectedAsync(AuthenticatorInfo authenticatorInfo)
        {
            var fcp = CreateFinalChallengeParams(RegistrationRequest, ChannelBinding, CallerPackageFamilyName);
            var register = new RegisterIn
            {
                AttestationType = authenticatorInfo.AttestationTypes,
                Username = RegistrationRequest.Username,
                AppId = RegistrationRequest.Header.AppId,
                FinalChallenge = fcp
            };

            var response = await asmApi.RegisterAsync(register, clientApi.authenticatorIdToPackageFamilyName[authenticatorInfo.Aaid], (ushort)authenticatorInfo.AuthenticatorIndex);

            var authenticationResponse = new RegistrationResponse
            {
                Header = RegistrationRequest.Header,
                FcParams = fcp,
                Assertions = new AuthenticatorRegistrationAssertion[]
                {
                    new AuthenticatorRegistrationAssertion
                    {
                        Assertion = response.Assertion,
                        AssertionScheme = response.AssertionScheme
                    }
                }
            };

            promise.TrySetResult(authenticationResponse);
        }

        public async Task OnCancelationAsync(ErrorCode errorCode = ErrorCode.UserCancelled)
        {
            await Task.Delay(0);
            promise.TrySetException(new FidoOperationErrorCodeException(errorCode));
        }
    }
}
