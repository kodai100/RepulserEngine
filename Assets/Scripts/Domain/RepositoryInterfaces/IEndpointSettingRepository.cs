using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface IEndpointSettingRepository
    {
        void Save(IEnumerable<EndpointSetting> pulseSettingList);
        IEnumerable<EndpointSetting> Load();
    }
    
}