using System.Threading.Tasks;

namespace UwpUaf.Client.Api
{
    public interface IClientApi
    {
        Task<DiscoveryData> DiscoverAsync();
    }
}
