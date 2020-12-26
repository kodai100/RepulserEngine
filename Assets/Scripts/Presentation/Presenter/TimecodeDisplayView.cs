using System;
using Ltc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    
    // Timecode decode process must be inside of datastore
    public class TimecodeDisplayView : MonoBehaviour, ITimecodeDisplayView
    {
        [SerializeField] TMP_Text label = null;
        [SerializeField] private TMP_Text dropFrameText;
        [SerializeField] private TMP_Text nonDropFrameText;
    
        [SerializeField] private int framerate = 30;
        [SerializeField] private Color runningColor = Color.red;
        [SerializeField] private Color stayingColor = new Color(0, 0.7058824f, 1);
        [SerializeField] private float stayThresholdSeconds = 0.1f;

        public Timecode currentTimecode;
    
        private bool running = false;
        private float stopElapsedTime = 0;
    
        private float time = 0;

        public void UpdateTimecode(Timecode timecode)
        {
            time = 0;
            currentTimecode = timecode;
        }

        private void Update()
        {

            time += Time.deltaTime;
            
            running = !(time > stayThresholdSeconds);

            if (currentTimecode.dropFrame)
            {
                dropFrameText?.gameObject.SetActive(true);
                nonDropFrameText?.gameObject.SetActive(false);
            }
            else
            {
                dropFrameText?.gameObject.SetActive(true);
                nonDropFrameText?.gameObject.SetActive(false);
            }
            
            label.text = $"{currentTimecode.ToString()}";

            label.color = running ? runningColor : stayingColor;
        }
    }

}