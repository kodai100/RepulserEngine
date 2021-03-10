using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    
    public class TimecodeSettingRepository : ITimecodeSettingRepository
    {

        private ITimecodeSettingDataStore timecodeSettingDataStore;
        public IEnumerable<TimecodeSetting> TimecodeSettingList => timecodeSettingDataStore.Load();

        public IObservable<IEnumerable<TimecodeSetting>> OnDataChangedAsObservable =>
            timecodeSettingDataStore.OnDataChangedAsObservable;
        
        public TimecodeSettingRepository(ITimecodeSettingDataStore timecodeSettingDataStore)
        {
            this.timecodeSettingDataStore = timecodeSettingDataStore;
        }

        public void Save(IEnumerable<TimecodeSetting> timecodeSettingList)
        {
            timecodeSettingDataStore.Save(timecodeSettingList);
        }

        public IEnumerable<TimecodeSetting> Load()
        {
            return timecodeSettingDataStore.Load();
        }
    }
}

