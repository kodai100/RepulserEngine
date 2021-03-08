using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface IEndpointSettingDataStore
    {
        void Save(IEnumerable<EndpointSetting> pulseSettingList);
        IEnumerable<EndpointSetting> Load();
        
        IObservable<IEnumerable<EndpointSetting>> OnDataChangedAsObservable { get; }
    }
    
}