using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;

namespace UwpUaf.Asm.Shared
{
    interface IOnCancelationHandler
    {
        Task OnCancelationAsync(StatusCode statusCode = StatusCode.UafAsmStatusUserCancelled);
    }
}