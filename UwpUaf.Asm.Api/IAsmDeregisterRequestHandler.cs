using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Asm.Api
{
    public interface IAsmDeregisterRequestHandler
    {
        Task<AsmResponseBase> ProcessDeregisterRequestAsync(AsmRequest<DeregisterIn> asmRequest);
    }
}
