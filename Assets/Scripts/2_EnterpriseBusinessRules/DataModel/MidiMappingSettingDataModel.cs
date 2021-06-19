using System;
using System.Net;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.DataModel
{
    [Serializable]
    public class MidiMappingSettingDataModel
    {
        [SerializeField] private int midiType;
        [SerializeField] private int number;
        [SerializeField] private string oscAddressConversion;

        public int MidiType => midiType;
        public int MidiNumber => number;
        public string OscAddressConversion => oscAddressConversion;

        public MidiMappingSettingDataModel(int midiType, int number, string oscAddressConversion)
        {
            this.midiType = midiType;
            this.number = number;
            this.oscAddressConversion = oscAddressConversion;
        }

        public MidiMappingSettingDataModel() : this(0, 0, "")
        {
        }
    }
}