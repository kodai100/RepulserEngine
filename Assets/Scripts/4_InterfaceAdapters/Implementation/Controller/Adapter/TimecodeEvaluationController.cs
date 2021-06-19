using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class TimecodeEvaluationController : ITimecodeEvaluationController, IDisposable
    {
        // TODO: ここで取得しない
        private ITimecodeSettingUseCase timecodeSettingUseCase;

        private IOverlayUseCase overlayUseCase;

        private CompositeDisposable disposable = new CompositeDisposable();

        public IObservable<string> OnTriggerPulsedAsObservable => onTriggerPulsedSubject;
        private Subject<string> onTriggerPulsedSubject = new Subject<string>();

        public TimecodeEvaluationController(ITimecodeDecodeUseCase timecodeDecoderRepository,
            ITimecodeSettingUseCase timecodeSettingUseCase, IOverlayUseCase overlayUseCase)
        {
            this.timecodeSettingUseCase = timecodeSettingUseCase;

            this.overlayUseCase = overlayUseCase;

            timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(OnTimecodeUpdated).AddTo(disposable);
        }

        private void OnTimecodeUpdated(TimecodeData timecode)
        {
            foreach (var timecodeSetting in timecodeSettingUseCase.Load())
            {
                if (timecodeSetting == null) continue;

                var state = timecodeSetting.Evaluate(timecode);

                if (state == PulseState.Pulse)
                {
                    overlayUseCase.Trigger(Color.red);
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