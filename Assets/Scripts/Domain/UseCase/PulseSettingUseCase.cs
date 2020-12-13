
using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class PulseSettingUseCase : IInitializable, IDisposable
    {

        private IPulseSettingListPresenter _pulseSettingListPresenter;
        private IPulseSettingRepository _pulseSettingRepository;
        
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public PulseSettingUseCase(IPulseSettingListPresenter pulseSettingListPresenter,
            IPulseSettingRepository pulseSettingRepository)
        {
            _pulseSettingListPresenter = pulseSettingListPresenter;

            _pulseSettingListPresenter.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                // _pulseSettingListPresenter.UpdateData();
                _pulseSettingRepository.Save(_pulseSettingListPresenter.PulseSettingList);

            }).AddTo(_disposable);
            
            _pulseSettingRepository = pulseSettingRepository;
        }

        public void Initialize()
        {
            var data = _pulseSettingRepository.Load();
            _pulseSettingListPresenter.SetData(data);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
    
}