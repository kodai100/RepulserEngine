using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public interface IMidiMappingSettingUseCase
    {
        void Update(IEnumerable<MidiMappingSettingViewModel> list);
        void Save();
        IEnumerable<MidiMappingSettingViewModel> GetCurrent();
        IEnumerable<MidiMappingSettingViewModel> Load();
        public IObservable<IEnumerable<MidiMappingSettingViewModel>> OnListRecreatedAsObservable { get; }
    }
}