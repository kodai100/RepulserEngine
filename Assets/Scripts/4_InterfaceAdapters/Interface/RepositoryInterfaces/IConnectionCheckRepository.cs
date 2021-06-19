using System.Net;
using System.Threading.Tasks;

namespace ProjectBlue.RepulserEngine.Repository
{

    public interface IConnectionCheckRepository
    {
        Task<bool> Check(IPAddress address);
    }

}