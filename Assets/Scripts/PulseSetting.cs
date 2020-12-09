using Ltc;
using UnityEngine;

public class PulseSetting
{
    private int _index;
    
    private string _oscAddress;
    private string _oscData;
    private string _overrideIP;
    
    private int timecodeHour;
    private int timecodeMinute;
    private int timecodeSecond;
    private int timecodeFrame;

    public string OscAddress => _oscAddress;
    public string OscData => _oscData;
    public string OverrideIP => _overrideIP;
    
    public Timecode Timecode => new Timecode{dropFrame = false, frame = timecodeFrame, hour = timecodeHour, minute = timecodeMinute, second = timecodeSecond};

    public PulseSetting(int index, string oscAddress, string oscData, string overrideIP, Timecode timecode)
    {
        _index = index;
        _oscAddress = oscAddress;
        _oscData = oscData;
        _overrideIP = overrideIP;
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
            PlayerPrefs.GetString($"OverrideIP_{index}"),
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
        PlayerPrefs.SetString($"OscAddress_{_index}", _oscAddress);
        PlayerPrefs.SetString($"OscData_{_index}", _oscData);
        PlayerPrefs.SetString($"OverrideIP_{_index}", _overrideIP);
        
        PlayerPrefs.SetInt($"Hour_{_index}", timecodeHour);
        PlayerPrefs.SetInt($"Minute_{_index}", timecodeMinute);
        PlayerPrefs.SetInt($"Second_{_index}", timecodeSecond);
        PlayerPrefs.SetInt($"Frame_{_index}", timecodeFrame);
    }
}
