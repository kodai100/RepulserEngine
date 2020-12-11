using System;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.Repository;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeDisplayUseCase : IDisposable
    {
        private ITimecodeDisplayPresenter _timecodeDisplayPresenter;
        private ITimecodeDecoderRepository timecodeDecoderRepository;

        private CompositeDisposable disposable = new CompositeDisposable();
        
        public TimecodeDisplayUseCase(ITimecodeDisplayPresenter timecodeDisplayPresenter, ITimecodeDecoderRepository timecodeDecoderRepository)
        {

            this._timecodeDisplayPresenter = timecodeDisplayPresenter;
            this.timecodeDecoderRepository = timecodeDecoderRepository;

            this.timecodeDecoderRepository.OnTimecodeUpdatedAsObservable.Subscribe(timecode =>
            {
                
                this._timecodeDisplayPresenter.UpdateTimecode(timecode);
                
            }).AddTo(disposable);

        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }

}

