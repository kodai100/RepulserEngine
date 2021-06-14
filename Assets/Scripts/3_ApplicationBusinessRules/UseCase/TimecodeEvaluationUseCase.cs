using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeEvaluationUseCase : ITimecodeEvaluationUseCase, IDisposable
    {
        private ITimecodeSettingRepository timecodeSettingRepository;

        private Subject<string> onPulsedSubject = new Subject<string>();
        private CompositeDisposable disposable = new CompositeDisposable();

        public IObservable<string> OnTriggerPulsedAsObservable => onPulsedSubject;

        public TimecodeEvaluationUseCase(ITimecodeSettingRepository timecodeSettingRepository,
            ITimecodeDecoderRepository timecodeDecoderRepository)
        {
            this.timecodeSettingRepository = timecodeSettingRepository;

            timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(OnTimecodeUpdated).AddTo(disposable);
        }

        private void OnTimecodeUpdated(TimecodeData timecode)
        {
            foreach (var timecodeSetting in timecodeSettingRepository.Load())
            {
                if (timecodeSetting == null) continue;

                var state = timecodeSetting.Evaluate(timecode);

                if (state == PulseState.Pulse)
                {
                    onPulsedSubject.OnNext(timecodeSetting.ConnectedCommandName);
                }
            }
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}