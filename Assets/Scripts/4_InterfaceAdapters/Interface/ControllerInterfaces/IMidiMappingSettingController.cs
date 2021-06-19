using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IMidiMappingSettingController
    {
        void Update(IEnumerable<MidiMappingSettingViewModel> settingList);
        void Save();
        IEnumerable<MidiMappingSettingViewModel> Load();
        IObservable<IEnumerable<MidiMappingSettingViewModel>> OnListRecreatedAsObservable { get; }
    }
}