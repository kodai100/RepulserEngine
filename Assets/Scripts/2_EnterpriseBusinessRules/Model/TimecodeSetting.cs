using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.Model
{

    [Serializable]
    public class TimecodeSetting
    {

        [SerializeField] private TimecodeData timecodeData;
        [SerializeField] private string connectedCommandName;

        public TimecodeData TimecodeData => timecodeData;
        public string ConnectedCommandName => connectedCommandName;

        public TimecodeSetting(TimecodeData timecodeData, string connectedCommandName)
        {
            this.timecodeData = timecodeData;
            this.connectedCommandName = connectedCommandName;
        }

        public TimecodeSetting() : this(new TimecodeData(), "NULL")
        {
            
        }

    }

}