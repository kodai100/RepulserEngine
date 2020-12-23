using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeEvaluationUseCase : IDisposable
    {
        private IPulseSettingListPresenter pulseSettingListPresenter;
        private IKeyboardInputRepository keyboardInputRepository;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        
        private Subject<OscMessage> onSendMessageSubject = new Subject<OscMessage>();
        public IObservable<OscMessage> OnSendMessageAsObservable => onSendMessageSubject;

        public TimecodeEvaluationUseCase(
            IPulseSettingListPresenter pulseSettingListPresenter,
            ITimecodeDecoderRepository timecodeDecoderRepository,
            IKeyboardInputRepository keyboardInputRepository)
        {
            
            this.pulseSettingListPresenter = pulseSettingListPresenter;
            this.keyboardInputRepository = keyboardInputRepository;

            keyboardInputRepository.OnInputAsObservable.Subscribe(key =>
            {
                foreach (var pulseSetting in pulseSettingListPresenter.PulseSettingList)
                {
                    if(pulseSetting == null) continue;
                    if(pulseSetting.SendKey == null) continue;
                    
                    if (key == pulseSetting.SendKey)
                    {
                        onSendMessageSubject.OnNext(new OscMessage(pulseSetting.OverrideIp, pulseSetting.OscAddress, pulseSetting.OscData));
                    }
                
                }
            }).AddTo(disposable);
            
            timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(OnTimecodeUpdated).AddTo(disposable);
            
        }

        private void OnTimecodeUpdated(Timecode timecode)
        {

            foreach (var pulseSetting in pulseSettingListPresenter.PulseSettingList)
            {
                
                if(pulseSetting == null) continue;

                var state = pulseSetting.Evaluate(timecode);

                if (state == PulseState.Pulse)
                {
                    onSendMessageSubject.OnNext(new OscMessage(pulseSetting.OverrideIp, pulseSetting.OscAddress, pulseSetting.OscData));
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

