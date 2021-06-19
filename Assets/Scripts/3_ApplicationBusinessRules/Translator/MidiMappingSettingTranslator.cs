using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Translators
{
    public class MidiMappingSettingTranslator
    {
        public static MidiMappingSettingDataModel Translate(MidiMappingSettingViewModel viewModel)
        {
            var dataModel = new MidiMappingSettingDataModel((int) viewModel.MidiType, viewModel.MidiNumber,
                viewModel.OscAddressConversion);
            return dataModel;
        }

        public static MidiMappingSettingViewModel Translate(MidiMappingSettingDataModel dataModel)
        {
            var viewModel = new MidiMappingSettingViewModel(
                (MidiType) Enum.ToObject(typeof(MidiType), dataModel.MidiType),
                dataModel.MidiNumber,
                dataModel.OscAddressConversion);
            return viewModel;
        }
    }
}