using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Entity;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.DataStore
{
    public class TimecodeDecoderDataStore : ITickable, IDisposable, ITimecodeDecoderDataStore
    {
        private TimecodeDecoder timecodeDecoder = new TimecodeDecoder();
        private DeviceSelector deviceSelector;

        private Subject<TimecodeData> onTimecodeUpdatedSubject = new Subject<TimecodeData>();
        public IObservable<TimecodeData> OnTimecodeUpdatedAsObservable => onTimecodeUpdatedSubject;

        private int counter = 0;

        private TimecodeData lastTwoTimecode = new TimecodeData();
        private TimecodeData lastTimecode = new TimecodeData();

        private TimecodeData currentTimecode = new TimecodeData();
        private TimecodeData lastEffectiveTimecode = new TimecodeData();

        private int thresholdFrames = 5;

        public TimecodeDecoderDataStore(DeviceSelector audioDeviceSelector)
        {
            this.deviceSelector = audioDeviceSelector;
        }

        public void Tick()
        {
            timecodeDecoder.ParseAudioData(deviceSelector.AudioDataSpan);

            var current = timecodeDecoder.LastTimecode;
            currentTimecode.Update(current.hour, current.minute, current.second, current.frame, current.dropFrame);

            if (lastTimecode != currentTimecode)
            {
                var lastFrameDifference = Mathf.Abs(currentTimecode.ToFrame(30) - lastTimecode.ToFrame(30));
                var lastTwoFrameDifference = Mathf.Abs(lastTimecode.ToFrame(30) - lastTwoTimecode.ToFrame(30));

                var effectiveDifference = Mathf.Abs(currentTimecode.ToFrame(30) - lastEffectiveTimecode.ToFrame(30));

                if (lastFrameDifference + lastTwoFrameDifference == 2 || effectiveDifference <= 5)
                {
                    onTimecodeUpdatedSubject.OnNext(currentTimecode);
                    lastEffectiveTimecode.Update(currentTimecode);
                }
                else
                {
                    // Debug.Log($"Frame skipped : {currentTimecode} - Last Effective : {lastEffectiveTimecode}");
                }

                lastTwoTimecode.Update(lastTimecode);
                lastTimecode.Update(currentTimecode);
            }
        }


        public void Dispose()
        {
            onTimecodeUpdatedSubject.Dispose();
        }
    }
}