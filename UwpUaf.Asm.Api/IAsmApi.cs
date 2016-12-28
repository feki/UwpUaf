using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Windows.ApplicationModel;

namespace UwpUaf.Asm.Api
{
    /// <summary>
    ///
    /// </summary>
    public interface IAsmApi
    {
        Task<IReadOnlyList<AppInfo>> DiscoverAsmAsync();

        Task<IEnumerable<AsmGetInfo>> GetInfoAsync();

        Task<GetInfoOut> GetInfoAsync(string asmPackageFamilyName);

        Task<RegisterOut> RegisterAsync(RegisterIn registerIn, string asmPackageFamilyName, ushort authenticatorIndex);

        Task DeregisterAsync(DeregisterIn deregisterIn, string asmPackageFamilyName, ushort authenticatorIndex);

        Task<AuthenticateOut> AuthenticateAsync(AuthenticateIn authenticateIn, string asmPackageFamilyName, ushort authenticatorIndex);
    }
}
