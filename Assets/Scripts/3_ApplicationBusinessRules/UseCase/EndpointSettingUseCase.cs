using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class EndpointSettingUseCase : IEndPointSettingUseCase
    {
        
        private IEndpointSettingRepository endpointSettingRepository;
        
        public EndpointSettingUseCase(IEndpointSettingRepository endpointSettingRepository)
        {
            this.endpointSettingRepository = endpointSettingRepository;
        }

        public IEnumerable<EndpointSetting> Load()
        {
            return endpointSettingRepository.Load();
        }
        
        public void Save(IEnumerable<EndpointSetting> settings)
        {
            endpointSettingRepository.Save(settings);
        }
    }
    
}