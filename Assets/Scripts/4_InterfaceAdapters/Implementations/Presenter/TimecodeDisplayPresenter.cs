using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public class TimecodeDisplayPresenter : ITimecodeDisplayPresenter, IDisposable
    {
        public IObservable<TimecodeData> OnUpdateTimecodeAsObservable => onUpdateTimcodeSubject;

        private Subject<TimecodeData> onUpdateTimcodeSubject = new Subject<TimecodeData>();
        
        private ITimecodeDisplayUseCase timecodeDisplayUseCase;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        
        public TimecodeDisplayPresenter(ITimecodeDisplayUseCase timecodeDisplayUseCase)
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