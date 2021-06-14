using System.Threading.Tasks;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IConnectionCheckController
    {
        Task<bool> Check(int endpointId);
    }
}