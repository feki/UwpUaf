using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using Windows.ApplicationModel;

namespace UwpUaf.Asm.Api
{
    public class AsmGetInfo
    {
        public AsmResponse<GetInfoOut> AsmResponse { get; internal set; }

        public AppInfo AppInfo { get; internal set; }
    }
}
