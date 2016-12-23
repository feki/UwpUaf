using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Asm.Api;

namespace UwpUaf.Asm.Shared.AsmApi
{
    class AsmProtocolRequestHandlers : IAsmProtocolRequestHandlers
    {
        private readonly IAuthenticatorFactory authenticatorFactory;

        public AsmProtocolRequestHandlers(IAuthenticatorFactory authenticator)
        {
            this.authenticatorFactory = authenticator;
        }

        public Task<AsmResponse<AuthenticateOut>> ProcessAuthenticateRequestAsync(AsmRequest<AuthenticateIn> asmRequest)
        {
            throw new NotImplementedException();
        }

        public Task<AsmResponseBase> ProcessDeregisterRequestAsync(AsmRequest<DeregisterIn> asmRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<AsmResponse<RegisterOut>> ProcessRegisterRequestAsync(AsmRequest<RegisterIn> asmRequest)
        {
            var response = new AsmResponse<RegisterOut>();
            try
            {
                var auth = authenticatorFactory.GetAuthenticatorInstance(asmRequest.AuthenticatorIndex);
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
            catch (Exception)
            {
                response.StatusCode = StatusCode.UafAsmStatusError;
            }

            return response;
        }
    }
}
