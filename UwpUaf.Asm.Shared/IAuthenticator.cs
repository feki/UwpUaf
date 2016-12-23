using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Windows.Storage.Streams;

namespace UwpUaf.Asm.Shared
{
    public interface IAuthenticator
    {
        AuthenticatorInfo GetAuthenticatorInfo();

        string Aaid { get; }

        string AssertionScheme { get; }

        string KeyId { get; }

        Task<RegisterOut> RegisterAsync(RegisterIn args);

        Task<AuthenticateOut> AuthenticateAsync(AuthenticateIn args);

        Task<IBuffer> SignAsync(IBuffer challenge);

        IBuffer GetPublicKey();
        IBuffer GetCertificate();
    }
}