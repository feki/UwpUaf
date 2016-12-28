using System.Collections.Generic;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Messages.Asm.Objects;

namespace UwpUaf.Client.Api
{
    public interface IClientApi
    {
        Task<DiscoveryData> DiscoverAsync();

        Task<IList<AuthenticatorInfo>> GetAvailableAuthenticatorsAsync();

        Task<RegistrationResponse> RegisterAsync(RegistrationRequest registrationRequest, ChannelBinding channelBinding);

        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest, ChannelBinding channelBinding);

        Task DeregisterAsync(DeregistrationRequest deregistrationRequest, ChannelBinding channelBinding);
    }
}
