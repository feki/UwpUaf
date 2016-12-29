using System.Linq;
using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Utils
{
    public class RegistrationUtils
    {
        public RegistrationRequest ParseRegistrationRequestUafMessage(string regRequesUafMsg)
        {
            return JsonConvert.DeserializeObject<RegistrationRequest[]>(regRequesUafMsg).First((rr) =>
            {
                return rr.Header.Upv.Major == 1 && rr.Header.Upv.Minor == 0;
            });
        }
    }
}
