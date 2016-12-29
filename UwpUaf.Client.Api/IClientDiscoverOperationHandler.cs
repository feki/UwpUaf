using System.Threading.Tasks;

namespace UwpUaf.Client.Api
{
    public interface IClientDiscoverOperationHandler
    {
        Task<DiscoveryData> ProcessDiscoverOperationAsync();
    }
}
