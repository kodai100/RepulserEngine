using System.Threading.Tasks;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IConnectionCheckPresenter
    {
        Task<bool> Check(int endpointId);
    }

}