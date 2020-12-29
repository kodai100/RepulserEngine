using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface IEndpointSettingRepository
    {
        IEnumerable<EndpointSetting> EndPointList { get; }
        void Save(IEnumerable<EndpointSetting> pulseSettingList);
        IEnumerable<EndpointSetting> Load();
        IObservable<IEnumerable<EndpointSetting>> OnDataChangedAsObservable { get; }
    }
    
}