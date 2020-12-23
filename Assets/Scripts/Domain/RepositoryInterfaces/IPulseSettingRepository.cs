using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public interface IPulseSettingRepository
    {
        IEnumerable<PulseSetting> PulseSettingList { get; }
        void Save(IEnumerable<PulseSetting> pulseSettingList);
        IEnumerable<PulseSetting> Load();
    }
    
}