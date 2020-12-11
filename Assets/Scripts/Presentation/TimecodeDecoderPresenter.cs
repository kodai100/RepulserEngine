using System;
using Ltc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.Presentation
{
    
    // Timecode decode process must be inside of datastore
    public class TimecodeDecoderPresenter : MonoBehaviour, ITimecodeDecodePresenter
    {
        [SerializeField] DeviceSelector _selector = null;
        [SerializeField] TMP_Text label = null;
        [SerializeField] private Text _dropFrameText;
    
        [SerializeField] private int framerate = 30;
        [SerializeField] private Color runningColor = Color.red;
        [SerializeField] private Color stayingColor = new Color(0, 0.7058824f, 1);
        [SerializeField] private float stayThresholdSeconds = 0.5f;
    
        TimecodeDecoder timecodeDecoder = new TimecodeDecoder();
    
        public IObservable<Timecode> OnTimecodeChangedAsObservable { get; }
    
        public Timecode CurrentTimecode { get; private set; }
        private Timecode lastTimecode;
    
        private bool running = false;
        private float stopElapsedTime = 0;
    
        private float time = 0;
        
        private void Update()
        {
            time += Time.deltaTime;
            
            timecodeDecoder.ParseAudioData(_selector.AudioDataSpan);
            
            CurrentTimecode = timecodeDecoder.LastTimecode;
            _dropFrameText.text = CurrentTimecode.dropFrame ? "Drop Frame" : "Non-Drop Frame";
            label.text = $"{CurrentTimecode.ToString()}";
    
            if (time > (1f / framerate))
            {
                TargetFrameratedUpdate(CurrentTimecode);
                time = 0;
            }
    
            label.color = running ? runningColor : stayingColor;
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

}