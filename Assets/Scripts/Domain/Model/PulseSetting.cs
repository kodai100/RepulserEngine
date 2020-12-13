using System;
using Ltc;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.Model
{

    public enum PulseState
    {
        Predecessor, Pulse, Successor
    }
    
    [Serializable]
    public class PulseSetting
    {
        [SerializeField] private string oscAddress;
        [SerializeField] private string oscData;
        
        [SerializeField] private int timecodeHour;
        [SerializeField] private int timecodeMinute;
        [SerializeField] private int timecodeSecond;
        [SerializeField] private int timecodeFrame;

        public PulseState PulseState { get; private set; } = PulseState.Predecessor;
        
        public string OscAddress => oscAddress;
        public string OscData => oscData;
        
        public Timecode Timecode => new Timecode{dropFrame = false, hour = timecodeHour, minute = timecodeMinute, second = timecodeSecond,  frame = timecodeFrame};

        public PulseSetting(string oscAddress, string oscData, Timecode timecode)
        {
            this.oscAddress = oscAddress;
            this.oscData = oscData;
            timecodeHour = timecode.hour;
            timecodeMinute = timecode.minute;
            timecodeSecond = timecode.second;
            timecodeFrame = timecode.frame;
        }

        public void UpdateData(string oscAddress, string oscData, Timecode timecode)
        {
            this.oscAddress = oscAddress;
            this.oscData = oscData;
            timecodeHour = timecode.hour;
            timecodeMinute = timecode.minute;
            timecodeSecond = timecode.second;
            timecodeFrame = timecode.frame;
        }
        
        public PulseState Evaluate(Timecode timecode)
        {
            
            if (timecode == Timecode)
            {
                Debug.Log($"Pulse : {Timecode}");
                PulseState = PulseState.Pulse;
                return PulseState;
            }
            
            if (timecode < Timecode)
            {
                PulseState = PulseState.Predecessor;
            }

            if (Timecode < timecode)
            {
                PulseState = PulseState.Successor;
            }

            return PulseState;    // TODO: Error code
        }
        
    }

}

