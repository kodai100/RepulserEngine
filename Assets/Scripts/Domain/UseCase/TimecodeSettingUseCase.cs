
using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class TimecodeSettingUseCase : IInitializable, IDisposable
    {

        private ITimecodeSettingListPresenter timecodeSettingListPresenter;
        private ITimecodeSettingRepository timecodeSettingRepository;
        
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public TimecodeSettingUseCase(ITimecodeSettingListPresenter timecodeSettingListPresenter, ITimecodeSettingRepository timecodeSettingRepository)
        {
            this.timecodeSettingListPresenter = timecodeSettingListPresenter;

            timecodeSettingListPresenter.OnSaveAsObservable.Subscribe(list =>
            {
                
                timecodeSettingRepository.Save(list);

            }).AddTo(_disposable);
            
            this.timecodeSettingRepository = timecodeSettingRepository;
        }

        public void Initialize()
        {
            var data = timecodeSettingRepository.Load();
            timecodeSettingListPresenter.SetData(data);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
    
}