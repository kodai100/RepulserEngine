using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.Model
{

    [Serializable]
    public class TimecodeSetting
    {

        [SerializeField] private TimecodeData timecodeData;
        [SerializeField] private string connectedCommandId;

        public TimecodeData TimecodeData => timecodeData;
        public string ConnectedCommandId => connectedCommandId;

        public TimecodeSetting(TimecodeData timecodeData, string connectedCommandId)
        {
            this.timecodeData = timecodeData;
            this.connectedCommandId = connectedCommandId;
        }

    }

}