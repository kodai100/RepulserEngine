using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeDisplayUseCase : ITimecodeDisplayUseCase, IDisposable
    {
        public IObservable<TimecodeData> OnTimecodeUpdatedAsObservable => onTimecodeUpdatedSubject;

        private Subject<TimecodeData> onTimecodeUpdatedSubject = new Subject<TimecodeData>();

        private ITimecodeDecoderRepository timecodeDecoderRepository;

        private CompositeDisposable disposable = new CompositeDisposable();

        private TimecodeData timecode = new TimecodeData();

        public TimecodeDisplayUseCase(ITimecodeDecoderRepository timecodeDecoderRepository)
        {
            this.timecodeDecoderRepository = timecodeDecoderRepository;

            this.timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(t =>
            {
                timecode.Update(t.hour, t.minute, t.second, t.frame, t.dropFrame);
                onTimecodeUpdatedSubject.OnNext(timecode);
            }).AddTo(disposable);
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}