using System.Threading.Tasks;

namespace UwpUaf.Client.Api
{
    public interface IOnConfirmationHandler : IOnCancelationHandler
    {
        Task OnConfirmationAsync();
    }
}