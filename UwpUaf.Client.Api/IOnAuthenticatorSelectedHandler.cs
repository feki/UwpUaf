using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Client.Api
{
    public interface IOnAuthenticatorSelectedHandler: IOnCancelationHandler
    {
        Task OnAuthenticatorSelectedAsync(AuthenticatorInfo authenticatorInfo);
    }
}