using System;
using Ltc;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.DataStore
{
    
    public class TimecodeDecoderDataStore : ITickable, IDisposable, ITimecodeDecoderDataStore
    {
        
        private TimecodeDecoder timecodeDecoder = new TimecodeDecoder();
        private DeviceSelector deviceSelector;

        private Subject<Timecode> onTimecodeUpdatedSubject = new Subject<Timecode>();
        public IObservable<Timecode> OnTimecodeUpdatedAsObserbable => onTimecodeUpdatedSubject;

        private int counter = 0;

        private Timecode lastTwoTimecode;
        private Timecode lastTimecode;
        private Timecode currentTimecode;
        private Timecode lastEffectiveTimecode;

        private int thresholdFrames = 5;
        
        public TimecodeDecoderDataStore(DeviceSelector audioDeviceSelector)
        {
            this.deviceSelector = audioDeviceSelector;
        }

        public void Tick()
        {
            
            timecodeDecoder.ParseAudioData(deviceSelector.AudioDataSpan);

            currentTimecode = timecodeDecoder.LastTimecode;

            if (lastTimecode != currentTimecode)
            {
                var lastFrameDifference = Mathf.Abs(currentTimecode.ToFrame(30) - lastTimecode.ToFrame(30));
                var lastTwoFrameDifference = Mathf.Abs(lastTimecode.ToFrame(30) - lastTwoTimecode.ToFrame(30));

                var effectiveDifference = Mathf.Abs(currentTimecode.ToFrame(30) - lastEffectiveTimecode.ToFrame(30));
                
                if (lastFrameDifference + lastTwoFrameDifference == 2 || effectiveDifference <= 5)
                {
                    onTimecodeUpdatedSubject.OnNext(currentTimecode);
                    lastEffectiveTimecode = currentTimecode;
                }
                else
                {
                    // Debug.Log($"Frame skipped : {currentTimecode} - Last Effective : {lastEffectiveTimecode}");
                }

                lastTwoTimecode = lastTimecode;
                lastTimecode = currentTimecode;
                
            }
            
        }
        

        public void Dispose()
        {
            onTimecodeUpdatedSubject.Dispose();
        }
    }
    
    

}