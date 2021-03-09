using System;
using System.Linq;
using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class ConnectionCheckUseCase : IConnectionCheckUseCase
    {
        private IConnectionCheckRepository connectionCheckRepository;
        private IEndpointSettingRepository endpointSettingRepository;

        public ConnectionCheckUseCase(
            IConnectionCheckRepository connectionCheckRepository,
            IEndpointSettingRepository endpointSettingRepository)
        {
            this.connectionCheckRepository = connectionCheckRepository;
            this.endpointSettingRepository = endpointSettingRepository;
        }
        
        public Task<bool> Check(int endpointId)
        {

            var list = endpointSettingRepository.Load().ToList();

            if (endpointId < list.Count)
            {
                return connectionCheckRepository.Check(list[endpointId].EndPoint.Address);
            }
            
            throw new IndexOutOfRangeException($"Invalid index was specified {endpointId}");
            
        }
    }

}