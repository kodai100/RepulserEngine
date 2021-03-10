using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.DataModel
{

    public enum PulseState
    {
        Predecessor, Pulse, Successor
    }
    
    [Serializable]
    public class TimecodeSetting
    {

        [SerializeField] private TimecodeData timecodeData;
        [SerializeField] private string connectedCommandName;

        public TimecodeData TimecodeData => timecodeData;
        public string ConnectedCommandName => connectedCommandName;
        
        public PulseState PulseState { get; private set; } = PulseState.Predecessor;

        public TimecodeSetting(TimecodeData timecodeData, string connectedCommandName)
        {
            this.timecodeData = timecodeData;
            this.connectedCommandName = connectedCommandName;
        }

        public TimecodeSetting() : this(new TimecodeData(), "NULL")
        {
            
        }
        
        public PulseState Evaluate(TimecodeData timecode)
        {
            
            if (timecode == timecodeData)
            {
                Debug.Log($"Pulse : {timecodeData}");
                PulseState = PulseState.Pulse;
                return PulseState;
            }
            
            if (timecode < timecodeData)
            {
                PulseState = PulseState.Predecessor;
            }

            if (timecodeData < timecode)
            {
                PulseState = PulseState.Successor;
            }

            return PulseState;    // TODO: Error code
        }


    }

}