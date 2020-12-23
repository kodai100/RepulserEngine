using System;
using Ltc;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeEvaluationUseCase : IDisposable
    {
        private IKeyboardInputRepository keyboardInputRepository;
        private IPulseSettingRepository pulseSettingRepository;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        
        private Subject<OscMessage> onSendMessageSubject = new Subject<OscMessage>();
        public IObservable<OscMessage> OnSendMessageAsObservable => onSendMessageSubject;

        public TimecodeEvaluationUseCase(
            IPulseSettingListPresenter pulseSettingListPresenter,
            ITimecodeDecoderRepository timecodeDecoderRepository,
            IKeyboardInputRepository keyboardInputRepository, 
            IPulseSettingRepository pulseSettingRepository
            )
        {
            
            this.keyboardInputRepository = keyboardInputRepository;
            this.pulseSettingRepository = pulseSettingRepository;

            keyboardInputRepository.OnInputAsObservable.Subscribe(key =>
            {
                foreach (var pulseSetting in pulseSettingRepository.PulseSettingList)
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

            foreach (var pulseSetting in pulseSettingRepository.PulseSettingList)
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

