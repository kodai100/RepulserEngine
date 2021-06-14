using System.Net;
using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class ConnectionCheckUseCase : IConnectionCheckUseCase
    {
        private IConnectionCheckRepository connectionCheckRepository;


        public ConnectionCheckUseCase(IConnectionCheckRepository connectionCheckRepository)
        {
            this.connectionCheckRepository = connectionCheckRepository;
        }

        public Task<bool> Check(IPAddress address)
        {
            return connectionCheckRepository.Check(address);
        }
    }
}