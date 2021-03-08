using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    
    // Timecode decode process must be inside of datastore
    public class TimecodeDisplayView : MonoBehaviour
    {

        [Inject] private ITimecodeDisplayPresenter timecodeDisplayPresenter;
        
        [SerializeField] TMP_Text label = null;
        [SerializeField] private TMP_Text dropFrameText;
        [SerializeField] private TMP_Text nonDropFrameText;
    
        [SerializeField] private int framerate = 30;
        [SerializeField] private Color runningColor = Color.red;
        [SerializeField] private Color stayingColor = new Color(0, 0.7058824f, 1);
        [SerializeField] private float stayThresholdSeconds = 0.1f;

        public TimecodeData currentTimecode;
    
        private bool running = false;
        private float stopElapsedTime = 0;
    
        private float time = 0;

        private void Start()
        {
            timecodeDisplayPresenter.OnUpdateTimecodeAsObservable.Subscribe(UpdateTimecode).AddTo(this);
        }
        
        public void UpdateTimecode(TimecodeData timecode)
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