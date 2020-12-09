using Ltc;

public class PulseSetting
{
    private readonly string _oscAddress;
    private readonly string _oscData;
    private readonly string _overrideIP;
    
    private int timecodeHour;
    private int timecodeMinute;
    private int timecodeSecond;
    private int timecodeFrame;

    public string OscAddress => _oscAddress;
    public string OscData => _oscData;
    public string OverrideIP => _overrideIP;
    
    public Timecode Timecode => new Timecode{dropFrame = false, frame = timecodeFrame, hour = timecodeHour, minute = timecodeMinute, second = timecodeSecond};

    public PulseSetting(string oscAddress, string oscData, string overrideIP, Timecode timecode)
    {
        this._oscAddress = oscAddress;
        this._oscData = oscData;
        this._overrideIP = overrideIP;
        this.timecodeHour = timecode.hour;
        this.timecodeMinute = timecode.minute;
        this.timecodeSecond = timecode.second;
        this.timecodeFrame = timecode.frame;
    }
}
