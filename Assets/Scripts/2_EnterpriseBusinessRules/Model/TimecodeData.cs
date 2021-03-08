using System;

namespace ProjectBlue.RepulserEngine.Domain.Model
{
    
    [Serializable]
    public class TimecodeData
    {
        public int hour;
        public int minute;
        public int second;
        public int frame;
        public bool dropFrame;

        public TimecodeData(int hour, int minute, int second, int frame, bool dropFrame)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.frame = frame;
            this.dropFrame = dropFrame;
        }

        public TimecodeData() : this(0, 0, 0, 0, false) { }

        public void Update(int hour, int minute, int second, int frame, bool dropFrame)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.frame = frame;
            this.dropFrame = dropFrame;
        }
        
        public void Update(TimecodeData data)
        {
            this.hour = data.hour;
            this.minute = data.minute;
            this.second = data.second;
            this.frame = data.frame;
            this.dropFrame = data.dropFrame;
        }

        public override string ToString()
        {
            return $"{hour:D2}:{minute:D2}:{second:D2};{frame:D2}";
        }
        
        public override bool Equals(object obj)
        {
            if (obj is TimecodeData)
            {
                return this.Equals((TimecodeData)obj);
            }
            return false;
        }

        private bool LargerThan(TimecodeData t)
        {
            if (hour > t.hour) return true;
            if (hour < t.hour) return false;

            if (minute > t.minute) return true;
            if (minute < t.minute) return false;

            if (second > t.second) return true;
            if (second < t.second) return false;

            if (frame > t.frame) return true;
            
            return false;
        }

        private bool Equals(TimecodeData t)
        {
            return hour == t.hour && minute == t.minute && second == t.second && frame == t.frame;
        }

        public static bool operator ==(TimecodeData lhs, TimecodeData rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(TimecodeData lhs, TimecodeData rhs)
        {
            return !lhs.Equals(rhs);
        }
        
        public static bool operator >(TimecodeData lhs, TimecodeData rhs)
        {
            return lhs.LargerThan(rhs);
        }

        public static bool operator <(TimecodeData lhs, TimecodeData rhs)
        {
            return !lhs.LargerThan(rhs);
        }

        public int ToFrame(int framerate)
        {
            return framerate * 60 * 60 * hour + framerate * 60 * minute + framerate * second + frame;
        }
    }
}