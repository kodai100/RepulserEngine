using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface ITimecodeSettingRepository
    {
        void Save(IEnumerable<TimecodeSetting> pulseSettingList);
        IEnumerable<TimecodeSetting> Load();
        IObservable<IEnumerable<TimecodeSetting>> OnDataChangedAsObservable { get; }
    }
    
}