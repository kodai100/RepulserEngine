
using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class EndpointSettingUseCase : IInitializable, IDisposable
    {

        private IEndPointListPresenter endPointListPresenter;
        private IEndpointSettingRepository endpointSettingRepository;
        
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public EndpointSettingUseCase(IEndPointListPresenter endPointListPresenter, IEndpointSettingRepository endpointSettingRepository)
        {
            this.endPointListPresenter = endPointListPresenter;

            endPointListPresenter.OnSaveAsObservable.Subscribe(list =>
            {
                
                endpointSettingRepository.Save(list);

            }).AddTo(_disposable);
            
            this.endpointSettingRepository = endpointSettingRepository;
        }

        public void Initialize()
        {
            var data = endpointSettingRepository.Load();
            endPointListPresenter.SetData(data);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
    
}