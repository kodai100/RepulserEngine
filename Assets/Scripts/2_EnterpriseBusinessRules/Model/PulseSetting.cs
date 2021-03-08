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

        [SerializeField] private string overrideIp;
        [SerializeField] private string sendkey;

        public PulseState PulseState { get; private set; } = PulseState.Predecessor;
        
        public string OscAddress => oscAddress;
        public string OscData => oscData;
        
        public TimecodeData Timecode => new TimecodeData(timecodeHour, timecodeMinute, timecodeSecond, timecodeFrame, false);

        public string OverrideIp => overrideIp;
        public string SendKey => sendkey;
        
        public PulseSetting(string oscAddress, string oscData, TimecodeData timecode, string overrideIp, string sendkey)
        {
            this.oscAddress = oscAddress;
            this.oscData = oscData;
            timecodeHour = timecode.hour;
            timecodeMinute = timecode.minute;
            timecodeSecond = timecode.second;
            timecodeFrame = timecode.frame;
            this.overrideIp = overrideIp;
            this.sendkey = sendkey;
        }

        public PulseSetting() : this("/NULL", "NULL", new TimecodeData(), "","")
        {
            
        }
        
        public void UpdateData(string oscAddress, string oscData, Timecode timecode, string overrideIp, string sendkey)
        {
            this.oscAddress = oscAddress;
            this.oscData = oscData;
            timecodeHour = timecode.hour;
            timecodeMinute = timecode.minute;
            timecodeSecond = timecode.second;
            timecodeFrame = timecode.frame;
            this.overrideIp = overrideIp;
            this.sendkey = sendkey;
        }
        
        public PulseState Evaluate(TimecodeData timecode)
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

