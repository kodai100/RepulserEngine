using System.Collections;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Entity;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.ViewModel
{
    public class MidiMappingSettingViewModel
    {
        public ReactiveProperty<MidiType> midiType = new ReactiveProperty<MidiType>();
        public ReactiveProperty<int> midiNumber = new ReactiveProperty<int>();
        public ReactiveProperty<string> oscAddressConversion = new ReactiveProperty<string>();

        public int prevValue = 0;

        public MidiType MidiType => midiType.Value;
        public int MidiNumber => midiNumber.Value;
        public string OscAddressConversion => oscAddressConversion.Value;

        public MidiMappingSettingViewModel(MidiType midiType, int midiNumber, string oscAddressConversion)
        {
            this.midiType.Value = midiType;
            this.midiNumber.Value = midiNumber;
            this.oscAddressConversion.Value = oscAddressConversion;
        }

        public MidiMappingSettingViewModel() : this(MidiType.Note, 0, "")
        {
        }
    }
}