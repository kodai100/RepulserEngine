using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface ITimecodeSettingDataStore
    {
        void Save(IEnumerable<TimecodeSetting> pulseSettingList);
        IEnumerable<TimecodeSetting> Load();
        
        IObservable<IEnumerable<TimecodeSetting>> OnDataChangedAsObservable { get; }
    }
    
}