using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface IMidiMappingSettingRepository
    {
        void Save(bool enabled, IEnumerable<MidiMappingSettingDataModel> pulseSettingList);
        (bool, IEnumerable<MidiMappingSettingDataModel>) Load();
    }
}