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

        private Timecode currentTimecode;
        private Timecode lastTimecode;
        
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
                onTimecodeUpdatedSubject.OnNext(currentTimecode);
            }

            lastTimecode = currentTimecode;
        }
        

        public void Dispose()
        {
            onTimecodeUpdatedSubject.Dispose();
        }
    }
    
    

}