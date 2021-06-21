using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public interface IMidiMappingSettingUseCase
    {
        void UpdateEnabled(bool isEnabled);
        void Update(IEnumerable<MidiMappingSettingViewModel> list);
        void Save();
        (bool, IEnumerable<MidiMappingSettingViewModel>) GetCurrent();
        (bool, IEnumerable<MidiMappingSettingViewModel>) Load();
        public IObservable<IEnumerable<MidiMappingSettingViewModel>> OnListRecreatedAsObservable { get; }
    }
}