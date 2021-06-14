using System.Net;
using System.Threading.Tasks;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public interface IConnectionCheckDataStore
    {
        Task<bool> Check(IPAddress address);
    }

}