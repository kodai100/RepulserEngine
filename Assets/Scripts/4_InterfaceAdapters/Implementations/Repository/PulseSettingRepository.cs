using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class PulseSettingRepository : IPulseSettingRepository
    {

        private IPulseSettingDataStore pulseSettingDataStore;

        public IEnumerable<PulseSetting> PulseSettingList => pulseSettingDataStore.Load();

        public PulseSettingRepository(IPulseSettingDataStore pulseSettingDataStore)
        {
            this.pulseSettingDataStore = pulseSettingDataStore;
        }

        public void Save(IEnumerable<PulseSetting> pulseSettingList)
        {
            pulseSettingDataStore.Save(pulseSettingList);
        }

        public IEnumerable<PulseSetting> Load()
        {
            return pulseSettingDataStore.Load();
        }
    }

}

