using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using TMPro;
using UnityEngine;
using UniRx;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.View
{
    public class MidiMappingSettingCellView : ReorderableListComponentView<MidiMappingSettingViewModel>
    {
        [SerializeField] private TMP_Dropdown midiTypeDropdown;

        [SerializeField] private TMP_InputField midiNumberTextInput;
        [SerializeField] private TMP_InputField oscConvertAddressTextInput;


        public override MidiMappingSettingViewModel Data => data;
        private MidiMappingSettingViewModel data = new MidiMappingSettingViewModel();

        private void Awake()
        {
            var array = Enum.GetValues(typeof(MidiType));
            var list = (from object item in array select item.ToString()).ToList();
            midiTypeDropdown.ClearOptions();
            midiTypeDropdown.AddOptions(list);
        }

        private void Start()
        {
            Observable.Merge(
                    midiNumberTextInput.OnValueChangedAsObservable().Skip(1),
                    midiTypeDropdown.OnValueChangedAsObservable().Select(value => value.ToString()).Skip(1),
                    oscConvertAddressTextInput.OnValueChangedAsObservable().Skip(1)
                )
                .Subscribe(value =>
                {
                    SetDirty();

                    data = ParseData(midiTypeDropdown.value, midiNumberTextInput.text, oscConvertAddressTextInput.text);
                }).AddTo(this);
        }

        private MidiMappingSettingViewModel ParseData(int midiType, string number, string oscAddress)
        {
            return new MidiMappingSettingViewModel();
        }

        public override void UpdateView(MidiMappingSettingViewModel viewModel)
        {
            this.data = viewModel;

            if (viewModel == null)
            {
                midiTypeDropdown.value = 0;
                midiNumberTextInput.text = "";
                oscConvertAddressTextInput.text = "";
                return;
            }

            // midiTypeDropdown.value = viewModel.MidiType;
            // midiNumberTextInput.text = viewModel.MidiNumber;
            // oscConvertAddressTextInput.text = viewModel.OscConvertAddress;
        }
    }
}