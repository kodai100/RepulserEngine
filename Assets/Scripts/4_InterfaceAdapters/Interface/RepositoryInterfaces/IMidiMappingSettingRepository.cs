using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface IMidiMappingSettingRepository
    {
        void Save(IEnumerable<MidiMappingSettingDataModel> pulseSettingList);
        IEnumerable<MidiMappingSettingDataModel> Load();
    }
}