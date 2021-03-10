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
    public class TimecodeEvaluationUseCase : IDisposable, ITimecodeEvaluationUseCase
    {
        private ITimecodeSettingRepository timecodeSettingRepository;

        private IOverlayPresenter overlayPresenter;
        
        private CompositeDisposable disposable = new CompositeDisposable();

        public IObservable<string> OnTriggerPulsedAsObservable => onTriggerPulsedSubject;
        private Subject<string> onTriggerPulsedSubject = new Subject<string>();

        public TimecodeEvaluationUseCase(
            ITimecodeDecoderRepository timecodeDecoderRepository,
            ITimecodeSettingRepository timecodeSettingRepository,
            IOverlayPresenter overlayPresenter
            )
        {
            
            this.timecodeSettingRepository = timecodeSettingRepository;

            this.overlayPresenter = overlayPresenter;
            
            
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
                    onTriggerPulsedSubject.OnNext(timecodeSetting.ConnectedCommandName);
                }
                
            }
        }

        public void Dispose()
        {
            disposable.Dispose();
            onTriggerPulsedSubject.Dispose();
        }
    }

}

