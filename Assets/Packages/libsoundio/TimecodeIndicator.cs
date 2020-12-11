using Ltc;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// LTC (linear timecode) indicator

public sealed class TimecodeIndicator : MonoBehaviour
{
    [SerializeField] DeviceSelector _selector = null;
    [SerializeField] TMP_Text _label = null;
    [SerializeField] private Text _dropFrameText;

    [SerializeField] private int framerate = 30;
    [SerializeField] private Color runningColor = Color.red;
    [SerializeField] private Color stayingColor = new Color(0, 0.7058824f, 1);
    [SerializeField] private float stayThresholdSeconds = 0.5f;

    TimecodeDecoder _decoder = new TimecodeDecoder();

    public Timecode CurrentTimecode { get; private set; }
    private Timecode lastTimecode;

    private bool running = false;
    private float stopElapsedTime = 0;

    private float time = 0;
    
    private void Update()
    {
        time += Time.deltaTime;
        
        _decoder.ParseAudioData(_selector.AudioDataSpan);
        
        CurrentTimecode = _decoder.LastTimecode;
        _dropFrameText.text = CurrentTimecode.dropFrame ? "Drop Frame" : "Non-Drop Frame";
        _label.text = $"{CurrentTimecode.ToString()}";

        if (time > (1f / framerate))
        {
            TargetFrameratedUpdate(CurrentTimecode);
            time = 0;
        }

        _label.color = running ? runningColor : stayingColor;
    }

    private void TargetFrameratedUpdate(Timecode currentTimecode)
    {
        if (running)
        {
            if (lastTimecode == currentTimecode)
            {
                stopElapsedTime += 1f / framerate;
            }
            
            if (stopElapsedTime > stayThresholdSeconds)
            {
                running = false;
            }
        }
        else
        {
            if (lastTimecode != currentTimecode)
            {
                running = true;
                stopElapsedTime = 0;
            }
        }

        lastTimecode = currentTimecode;
    }
}
