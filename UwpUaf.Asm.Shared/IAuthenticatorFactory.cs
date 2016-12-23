using System.Collections.Generic;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Asm.Shared
{
    public interface IAuthenticatorFactory
    {
        IList<AuthenticatorInfo> GetAuthenticatorInfoList();

        IAuthenticator GetAuthenticatorInstance(ushort authenticatorIndex);
    }
}