using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeEvaluationUseCase : IDisposable
    {
        private IKeyboardInputRepository keyboardInputRepository;
        private ITimecodeSettingRepository timecodeSettingRepository;

        private IOverlayPresenter overlayPresenter;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        
        private Subject<OscMessage> onSendMessageSubject = new Subject<OscMessage>();

        public TimecodeEvaluationUseCase(
            ITimecodeDecoderRepository timecodeDecoderRepository,
            IKeyboardInputRepository keyboardInputRepository, 
            ITimecodeSettingRepository timecodeSettingRepository,
            IOverlayPresenter overlayPresenter
            )
        {
            
            this.keyboardInputRepository = keyboardInputRepository;
            this.timecodeSettingRepository = timecodeSettingRepository;

            this.overlayPresenter = overlayPresenter;
            // // キータイプでの送信
            // keyboardInputRepository.OnInputAsObservable.Subscribe(key =>
            // {
            //     foreach (var timecodeSetting in timecodeSettingRepository.Load())
            //     {
            //         if(timecodeSetting == null) continue;
            //         // if (key == pulseSetting.SendKey)
            //         // {
            //         //     // onSendMessageSubject.OnNext(new OscMessage(pulseSetting.OverrideIp, pulseSetting.OscAddress, pulseSetting.OscData));
            //         // }
            //     
            //     }
            // }).AddTo(disposable);
            
            timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(OnTimecodeUpdated).AddTo(disposable);
            
        }

        private void OnTimecodeUpdated(TimecodeData timecode)
        {

            foreach (var timecodeSetting in timecodeSettingRepository.Load())
            {
                
                if(timecodeSetting == null) continue;

                var state = timecodeSetting.Evaluate(timecode);

                if (state == PulseState.Pulse)
                {
                    overlayPresenter.Trigger(Color.red);
                    // onSendMessageSubject.OnNext(new OscMessage(timecodeSetting.OverrideIp, timecodeSetting.OscAddress, timecodeSetting.OscData));
                }
                
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
            onSendMessageSubject.Dispose();
        }
    }

}

