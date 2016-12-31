using System;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Asm.Api;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Asm.Shared.AsmApi
{
    public class AsmProtocolRequestHandlers : IAsmProtocolRequestHandlers
    {
        readonly IAuthenticatorFactory authenticatorFactory;
        readonly Frame frame;

        public AsmProtocolRequestHandlers(IAuthenticatorFactory authenticator, Frame frame)
        {
            this.authenticatorFactory = authenticator;
            this.frame = frame;
        }

        public async Task<AsmResponse<AuthenticateOut>> ProcessAuthenticateRequestAsync(AsmRequest<AuthenticateIn> asmRequest)
        {
            var response = new AsmResponse<AuthenticateOut>();
            try
            {
                var auth = authenticatorFactory.GetAuthenticatorInstance(asmRequest.AuthenticatorIndex);
                auth.Frame = frame;
                var authenticateOut = await auth.AuthenticateAsync(asmRequest.Args);
                if (authenticateOut != null)
                {
                    response.ResponseData = authenticateOut;
                    response.StatusCode = StatusCode.UafAsmStatusOk;
                }
                else
                {
                    response.StatusCode = StatusCode.UafAsmStatusError;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = ex is UafAsmStatusException ? ((UafAsmStatusException)ex).StatusCode : StatusCode.UafAsmStatusError;
            }

            return response;
        }

        public async Task<AsmResponseBase> ProcessDeregisterRequestAsync(AsmRequest<DeregisterIn> asmRequest)
        {
            var response = new AsmResponseBase();
            try
            {
                var auth = authenticatorFactory.GetAuthenticatorInstance(asmRequest.AuthenticatorIndex);
                auth.Frame = frame;
                response.StatusCode = await auth.DeregisterAsync(asmRequest.Args) ? StatusCode.UafAsmStatusOk : StatusCode.UafAsmStatusError;
            }
            catch (Exception ex)
            {
                response.StatusCode = ex is UafAsmStatusException ? ((UafAsmStatusException)ex).StatusCode : StatusCode.UafAsmStatusError;
            }

            return response;
        }

        public async Task<AsmResponse<RegisterOut>> ProcessRegisterRequestAsync(AsmRequest<RegisterIn> asmRequest)
        {
            var response = new AsmResponse<RegisterOut>();
            try
            {
                var auth = authenticatorFactory.GetAuthenticatorInstance(asmRequest.AuthenticatorIndex);
                auth.Frame = frame;
                var registerOut = await auth.RegisterAsync(asmRequest.Args);
                if (registerOut != null)
                {
                    response.ResponseData = registerOut;
                    response.StatusCode = StatusCode.UafAsmStatusOk;
                }
                else
                {
                    response.StatusCode = StatusCode.UafAsmStatusError;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = ex is UafAsmStatusException ? ((UafAsmStatusException)ex).StatusCode : StatusCode.UafAsmStatusError;
            }

            return response;
        }
    }
}
