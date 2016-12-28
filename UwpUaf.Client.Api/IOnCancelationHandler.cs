using System.Threading.Tasks;

namespace UwpUaf.Client.Api
{
    public interface IOnCancelationHandler
    {
        Task OnCancelationAsync(ErrorCode errorCode = ErrorCode.UserCancelled);
    }
}