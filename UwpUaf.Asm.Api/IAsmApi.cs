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

        Task<AsmResponse<GetInfoOut>> GetInfoAsync(string asmPackageFamilyName);
    }
}
