using System.Linq;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Asm.Api;
using UwpUaf.Asm.Shared;

namespace UwpUaf.Asm.RtC
{
    class AsmServiceRequestHandlers: IAsmServiceRequestHandlers, IAsmGetInfoRequestHandler
    {
        readonly IAuthenticatorFactory authenticatorFactory;

        public AsmServiceRequestHandlers()
        {
            authenticatorFactory = new AuthenticatorFactory();
        }

        public async Task<AsmResponse<GetInfoOut>> ProcessGetInfoRequestAsync(AsmRequestBase asmRequest)
        {
            const StatusCode statusCode = StatusCode.UafAsmStatusOk;

            var list = authenticatorFactory.GetAuthenticatorInfoList();
            var getInfoOut = new GetInfoOut
            {
                Authenticators = list.ToArray()
            };

            var asmResponse = new AsmResponse<GetInfoOut>
            {
                StatusCode = statusCode,
                ResponseData = getInfoOut
            };

            return await Task.FromResult(asmResponse);
        }
    }
}
