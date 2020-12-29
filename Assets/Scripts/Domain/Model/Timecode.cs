using System;

namespace ProjectBlue.RepulserEngine.Domain.Model
{
    
    [Serializable]
    public class TimecodeData
    {
        public int hour = 0;
        public int minute = 0;
        public int second = 0;
        public int frame = 0;

        public TimecodeData(int hour, int minute, int second, int frame)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.frame = frame;
        }
    }
}