using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Asm.Shared
{
    public class AuthenticatorFactory: IAuthenticatorFactory
    {
        static readonly IList<IAuthenticator> authenticators = new List<IAuthenticator>
        {
            HelloAuthenticator.Instance,
            FingerprintAutenticatorSimulator.Instance
        };

        public IList<AuthenticatorInfo> GetAuthenticatorInfoList()
        {
            return authenticators.Select(a => a.GetAuthenticatorInfo()).ToList();
        }

        public IAuthenticator GetAuthenticatorInstance(ushort authenticatorIndex)
        {
            return authenticators.First(a => a.GetAuthenticatorInfo().AuthenticatorIndex == authenticatorIndex);
        }
    }
}
