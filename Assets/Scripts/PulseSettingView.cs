using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PulseSettingView : MonoBehaviour
{

    // TODO capsule
    [SerializeField] public InputField oscAddressField;
    [SerializeField] public InputField oscDataField;

    [SerializeField] public InputField overrideIpField;

    [SerializeField] public InputField timecodeHourInputField;
    [SerializeField] public InputField timecodeMinuteInputField;
    [SerializeField] public InputField timecodeSecondInputField;
    [SerializeField] public InputField timecodeFrameInputField;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color beforeColor = new Color(0, 0.5f, 0.5f);
    [SerializeField] private Color afterColor = new Color(0.2f, 0.2f, 0.2f);
    
    public IObservable<string> HourAsObservable => timecodeHourInputField.OnValueChangedAsObservable();
    public IObservable<string> MinuteAsObservable => timecodeMinuteInputField.OnValueChangedAsObservable();
    public IObservable<string> SecondAsObservable => timecodeSecondInputField.OnValueChangedAsObservable();
    public IObservable<string> FrameAsObservable => timecodeFrameInputField.OnValueChangedAsObservable();

    public IObservable<string> OscAddressAsObservable => oscAddressField.OnValueChangedAsObservable();
    public IObservable<string> OscDataAsObservable => oscDataField.OnValueChangedAsObservable();
    public IObservable<string> OverrideIpAsObservable => overrideIpField.OnValueChangedAsObservable();

    // private Material mat;

    public enum State
    {
        Initialize, Before, Pulse, After
    }

    private State state = State.Initialize;
    
    private void Start()
    {
        // mat = (Material)Instantiate(new Material(Shader.Find("UI/Default")));
        // backgroundImage.material = mat;
    }

    public void SetData(PulseSetting pulseSetting)
    {
        oscAddressField.text = pulseSetting.OscAddress;
        oscDataField.text = pulseSetting.OscData;

        overrideIpField.text = pulseSetting.OverrideIP;

        timecodeHourInputField.text = pulseSetting.Timecode.hour.ToString();
        timecodeMinuteInputField.text = pulseSetting.Timecode.minute.ToString();
        timecodeSecondInputField.text = pulseSetting.Timecode.second.ToString();
        timecodeFrameInputField.text = pulseSetting.Timecode.frame.ToString();
    }

    public void SetBefore()
    {
        if (state == State.Before) return;
        backgroundImage.color = beforeColor;
        state = State.Before;

    }
    
    public void SetPulse()
    {
        if (state == State.Pulse) return;
        backgroundImage.color = Color.red;
        state = State.Pulse;
    }

    public void SetAfter()
    {
        if (state == State.After) return;
        backgroundImage.color = afterColor;
        state = State.After;
    }
    
}
