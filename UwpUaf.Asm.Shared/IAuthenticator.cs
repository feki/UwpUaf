using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace UwpUaf.Asm.Shared
{
    public interface IAuthenticator
    {
        Frame Frame { set; }

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