using System.Threading.Tasks;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface IConnectionCheckUseCase
    {
        Task<bool> Check(int endpointId);
    }
}