using Ltc;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    public class PulseSetting
    {
        private int index;
        
        private string oscAddress;
        private string oscData;
        
        private int timecodeHour;
        private int timecodeMinute;
        private int timecodeSecond;
        private int timecodeFrame;

        public string OscAddress => oscAddress;
        public string OscData => oscData;
        
        public Timecode Timecode => new Timecode{dropFrame = false, hour = timecodeHour, minute = timecodeMinute, second = timecodeSecond,  frame = timecodeFrame};

        public PulseSetting(int index, string oscAddress, string oscData, Timecode timecode)
        {
            this.index = index;
            this.oscAddress = oscAddress;
            this.oscData = oscData;
            timecodeHour = timecode.hour;
            timecodeMinute = timecode.minute;
            timecodeSecond = timecode.second;
            timecodeFrame = timecode.frame;
        }

        public static PulseSetting Load(int index)
        {
            
            var pulseSetting = new PulseSetting(
                index,
                PlayerPrefs.GetString($"OscAddress_{index}"),
                PlayerPrefs.GetString($"OscData_{index}"),
                new Timecode
                    {
                        dropFrame = false,
                        hour = PlayerPrefs.GetInt($"Hour_{index}"),
                        minute = PlayerPrefs.GetInt($"Minute_{index}"),
                        second = PlayerPrefs.GetInt($"Second_{index}"),
                        frame = PlayerPrefs.GetInt($"Frame_{index}")
                    }
                );

            return pulseSetting;
        }

        public void Save()
        {
            PlayerPrefs.SetString($"OscAddress_{index}", oscAddress);
            PlayerPrefs.SetString($"OscData_{index}", oscData);
            
            PlayerPrefs.SetInt($"Hour_{index}", timecodeHour);
            PlayerPrefs.SetInt($"Minute_{index}", timecodeMinute);
            PlayerPrefs.SetInt($"Second_{index}", timecodeSecond);
            PlayerPrefs.SetInt($"Frame_{index}", timecodeFrame);
        }
    }

}

