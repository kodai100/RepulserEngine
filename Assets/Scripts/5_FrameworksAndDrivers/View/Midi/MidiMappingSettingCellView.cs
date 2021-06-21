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
            var array = Enum.GetValues(typeof(MidiSendType));
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

                    if (!ParseData(midiNumberTextInput.text))
                    {
                        Invalid();
                    }
                }).AddTo(this);
        }

        private bool ParseData(string number)
        {
            data.midiSendType.Value = (MidiSendType) Enum.ToObject(typeof(MidiSendType), midiTypeDropdown.value);
            data.oscAddressConversion.Value = oscConvertAddressTextInput.text;

            if (int.TryParse(number, out var parsed))
            {
                data.midiNumber.Value = parsed;
                return true;
            }

            return false;
        }

        public override void UpdateView(MidiMappingSettingViewModel viewModel)
        {
            data = viewModel;

            if (viewModel == null)
            {
                midiTypeDropdown.value = 0;
                midiNumberTextInput.text = "";
                oscConvertAddressTextInput.text = "";
                return;
            }

            midiTypeDropdown.value = (int) viewModel.MidiSendType;
            midiNumberTextInput.text = viewModel.MidiNumber.ToString();
            oscConvertAddressTextInput.text = viewModel.OscAddressConversion;
        }
    }
}