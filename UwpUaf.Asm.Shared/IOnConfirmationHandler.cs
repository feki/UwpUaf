using System.Threading.Tasks;

namespace UwpUaf.Asm.Shared
{
    interface IOnConfirmationHandler : IOnCancelationHandler
    {
        Task OnConfirmationAsync();
    }
}
