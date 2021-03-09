using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface IEndpointSettingDataStore
    {
        void Save(IEnumerable<EndpointSettingDataModel> pulseSettingList);
        IEnumerable<EndpointSettingDataModel> Load();
    }
    
}