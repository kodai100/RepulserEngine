using System.Net;
using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public class ConnectionCheckPresenter : IConnectionCheckPresenter
    {
        private IConnectionCheckUseCase connectionCheckUseCase;

        public ConnectionCheckPresenter(IConnectionCheckUseCase connectionCheckUseCase)
        {
            this.connectionCheckUseCase = connectionCheckUseCase;
        }
        
        public Task<bool> Check(IPAddress address)
        {
            return connectionCheckUseCase.Check(address);
        }
    }

}