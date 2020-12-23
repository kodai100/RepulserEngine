
using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class PulseSettingUseCase : IInitializable, IDisposable
    {

        private IPulseSettingListPresenter pulseSettingListPresenter;
        private IPulseSettingRepository pulseSettingRepository;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        private CompositeDisposable saveButtonRegistrationDisposable = new CompositeDisposable();
        
        private Subject<OscMessage> onSendTriggeredSubject = new Subject<OscMessage>();
        public IObservable<OscMessage> OnSendTriggeredAsObservable => onSendTriggeredSubject;

        public PulseSettingUseCase(IPulseSettingListPresenter pulseSettingListPresenter, IPulseSettingRepository pulseSettingRepository)
        {
            this.pulseSettingListPresenter = pulseSettingListPresenter;
            this.pulseSettingRepository = pulseSettingRepository;

            this.pulseSettingListPresenter.OnSaveAsObservable.Subscribe(list =>
            {
                // _pulseSettingListPresenter.UpdateData();
                this.pulseSettingRepository.Save(list);

            }).AddTo(disposable);

        }
        
        public void Initialize()
        {
            var data = pulseSettingRepository.Load();
            pulseSettingListPresenter.SetData(data);
        }

        public void Dispose()
        {
            disposable.Dispose();
            onSendTriggeredSubject.Dispose();
        }

    }
    
}