using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IMidiMappingSettingController
    {
        void UpdateEnabled(bool isEnabled);
        void Update(IEnumerable<MidiMappingSettingViewModel> settingList);
        void Save();
        (bool, IEnumerable<MidiMappingSettingViewModel>) Load();
        IObservable<IEnumerable<MidiMappingSettingViewModel>> OnListRecreatedAsObservable { get; }
    }
}