using System;
using Ltc;
using UnityEngine;

public class PulseSettingPresenter : MonoBehaviour
{
    // TODO clean architecture
    [SerializeField] private PulseSettingView _pulseSettingView;

    private bool alreadyPulsed = false;

    private PulseSetting pulseSetting = null;

    public void Evaluate(Timecode timecode)
    {
        if (pulseSetting == null) return;

        if (timecode == pulseSetting.Timecode)
        {
            Pulse();
        }
    }

    public void Load()
    {
        
    }
    
    public void Save()
    {
        pulseSetting = new PulseSetting(
            _pulseSettingView.oscAddressField.text,
            _pulseSettingView.oscDataField.text,
            _pulseSettingView.overrideIpField.text,
            new Timecode
            {
                dropFrame = false,
                frame = int.Parse(_pulseSettingView.timecodeFrameInputField.text),
                hour = int.Parse(_pulseSettingView.timecodeHourInputField.text),
                minute = int.Parse(_pulseSettingView.timecodeMinuteInputField.text),
                second = int.Parse(_pulseSettingView.timecodeSecondInputField.text),
            }
        );
    }
    
    private void Pulse()
    {
        if (alreadyPulsed || pulseSetting == null) return;

        if (String.IsNullOrEmpty(pulseSetting.OverrideIP))
        {
            SenderAssembly.Instance.Send(pulseSetting.OscAddress, pulseSetting.OscData);
        }
        else
        {
            SenderAssembly.Instance.Send(pulseSetting.OverrideIP, pulseSetting.OscAddress, pulseSetting.OscData);
        }

        alreadyPulsed = true;
    }

}
