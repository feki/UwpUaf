using Fido.Uaf.Shared.Messages.Asm.Objects;
using Windows.ApplicationModel;

namespace UwpUaf.Asm.Api
{
    public class AsmGetInfo
    {
        public GetInfoOut GetInfoOut { get; internal set; }

        public AppInfo AppInfo { get; internal set; }
    }
}
