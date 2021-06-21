using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Entity;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.ViewModel
{
    public class MidiMappingSettingViewModel
    {
        public ReactiveProperty<MidiSendType> midiSendType = new ReactiveProperty<MidiSendType>();
        public ReactiveProperty<int> midiNumber = new ReactiveProperty<int>();
        public ReactiveProperty<string> oscAddressConversion = new ReactiveProperty<string>();

        public int prevValue = 0;

        public MidiSendType MidiSendType => midiSendType.Value;
        public int MidiNumber => midiNumber.Value;
        public string OscAddressConversion => oscAddressConversion.Value;

        public MidiMappingSettingViewModel(MidiSendType midiSendType, int midiNumber, string oscAddressConversion)
        {
            this.midiSendType.Value = midiSendType;
            this.midiNumber.Value = midiNumber;
            this.oscAddressConversion.Value = oscAddressConversion;
        }

        public MidiMappingSettingViewModel() : this(MidiSendType.Bypass, 0, "")
        {
        }
    }
}