using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class TimecodeDisplayController : ITimecodeDisplayController, IDisposable
    {
        public IObservable<TimecodeData> OnUpdateTimecodeAsObservable => onUpdateTimcodeSubject;

        private Subject<TimecodeData> onUpdateTimcodeSubject = new Subject<TimecodeData>();

        private ITimecodeDisplayUseCase timecodeDisplayUseCase;

        private CompositeDisposable disposable = new CompositeDisposable();

        public TimecodeDisplayController(ITimecodeDisplayUseCase timecodeDisplayUseCase)
        {
            this.timecodeDisplayUseCase = timecodeDisplayUseCase;

            this.timecodeDisplayUseCase.OnTimecodeUpdatedAsObservable.Subscribe(t =>
            {
                onUpdateTimcodeSubject.OnNext(t);
            }).AddTo(disposable);
        }


        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}