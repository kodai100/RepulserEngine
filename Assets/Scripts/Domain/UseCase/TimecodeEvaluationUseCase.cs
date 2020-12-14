using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeEvaluationUseCase : IDisposable
    {
        private IPulseSettingListPresenter _pulseSettingListPresenter;
        private ITimecodeDecoderRepository timecodeDecoderRepository;

        private SendToEndpointUseCase sendToEndpointUseCase;
        
        private CompositeDisposable disposable = new CompositeDisposable();

        public TimecodeEvaluationUseCase(SendToEndpointUseCase sendToEndpointUseCase, IPulseSettingListPresenter pulseSettingListPresenter, ITimecodeDecoderRepository timecodeDecoderRepository)
        {
            this.sendToEndpointUseCase = sendToEndpointUseCase;
            this._pulseSettingListPresenter = pulseSettingListPresenter;
            this.timecodeDecoderRepository = timecodeDecoderRepository;

            timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(timecode =>
            {
                OnTimecodeUpdated(timecode);
            }).AddTo(disposable);
            
        }

        private void OnTimecodeUpdated(Timecode timecode)
        {

            foreach (var pulseSetting in _pulseSettingListPresenter.PulseSettingList)
            {
                
                if(pulseSetting == null) continue;

                var state = pulseSetting.Evaluate(timecode);

                if (state == PulseState.Pulse)
                {
                    if (String.IsNullOrEmpty(pulseSetting.OverrideIp))
                        sendToEndpointUseCase.Send(pulseSetting.OscAddress, pulseSetting.OscData);
                    else
                        sendToEndpointUseCase.Send(pulseSetting.OscAddress, pulseSetting.OscData, pulseSetting.OverrideIp);
                }
                
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }

}

