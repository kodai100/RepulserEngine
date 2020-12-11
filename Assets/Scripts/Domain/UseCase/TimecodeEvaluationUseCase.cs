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
        private ISignalPulserPresenter signalPulserPresenter;
        private ITimecodeDecoderRepository timecodeDecoderRepository;

        private SendToEndpointUseCase sendToEndpointUseCase;
        
        private CompositeDisposable disposable = new CompositeDisposable();

        public TimecodeEvaluationUseCase(SendToEndpointUseCase sendToEndpointUseCase, ISignalPulserPresenter signalPulserPresenter, ITimecodeDecoderRepository timecodeDecoderRepository)
        {
            this.sendToEndpointUseCase = sendToEndpointUseCase;
            this.signalPulserPresenter = signalPulserPresenter;
            this.timecodeDecoderRepository = timecodeDecoderRepository;

            timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(timecode =>
            {
                OnTimecodeUpdated(timecode);
            }).AddTo(disposable);

        }

        private void OnTimecodeUpdated(Timecode timecode)
        {

            foreach (var pulseSetting in signalPulserPresenter.PulseSettingList)
            {
                
                if(pulseSetting == null) continue;

                var state = pulseSetting.Evaluate(timecode);

                if (state == PulseState.Pulse)
                {
                    sendToEndpointUseCase.Send(pulseSetting.OscAddress, pulseSetting.OscData);
                }
                
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }

}

