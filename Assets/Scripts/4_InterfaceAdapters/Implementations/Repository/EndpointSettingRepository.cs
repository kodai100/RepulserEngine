using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    
    public class EndpointSettingRepository : IEndpointSettingRepository
    {

        private IEndpointSettingDataStore endpointSettingDataStore;
        public IEnumerable<EndpointSettingDataModel> EndPointList => endpointSettingDataStore.Load();

        public EndpointSettingRepository(IEndpointSettingDataStore endpointSettingDataStore)
        {
            this.endpointSettingDataStore = endpointSettingDataStore;
        }

        public void Save(IEnumerable<EndpointSettingDataModel> endpointSettingList)
        {
            endpointSettingDataStore.Save(endpointSettingList);
        }

        public IEnumerable<EndpointSettingDataModel> Load()
        {
            return endpointSettingDataStore.Load();
        }
    }

}

