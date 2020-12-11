using System;
using Ltc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.Presentation
{
    
    // Timecode decode process must be inside of datastore
    public class TimecodeDisplayPresenter : MonoBehaviour, ITimecodeDisplayPresenter
    {
        [SerializeField] TMP_Text label = null;
        [SerializeField] private Text _dropFrameText;
    
        [SerializeField] private int framerate = 30;
        [SerializeField] private Color runningColor = Color.red;
        [SerializeField] private Color stayingColor = new Color(0, 0.7058824f, 1);
        [SerializeField] private float stayThresholdSeconds = 0.5f;

        public Timecode currentTimecode;
        private Timecode lastTimecode;
    
        private bool running = false;
        private float stopElapsedTime = 0;
    
        private float time = 0;

        public void UpdateTimecode(Timecode timecode)
        {
            time += Time.deltaTime;
            
            currentTimecode = timecode;
            
            _dropFrameText.text = currentTimecode.dropFrame ? "Drop Frame" : "Non-Drop Frame";
            label.text = $"{currentTimecode.ToString()}";

            label.color = running ? runningColor : stayingColor;
        }

        private void Update()
        {
            if (lastTimecode == currentTimecode)
            {
                time += Time.deltaTime;

                if (time > stayThresholdSeconds)
                {
                    running = false;
                }
                
            }
            else
            {
                time = 0;
                running = true;
            }
        }
    }

}