
using System;
using ProjectBlue.RepulserEngine.DataStore;
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
        private SendToEndpointUseCase sendToEndpointUseCase;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        private CompositeDisposable dispose = new CompositeDisposable();

        public PulseSettingUseCase(SendToEndpointUseCase sendToEndpointUseCase,
            IPulseSettingListPresenter pulseSettingListPresenter, IPulseSettingRepository pulseSettingRepository)
        {
            this.pulseSettingListPresenter = pulseSettingListPresenter;
            this.sendToEndpointUseCase = sendToEndpointUseCase;
            this.pulseSettingRepository = pulseSettingRepository;

            this.pulseSettingListPresenter.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                // _pulseSettingListPresenter.UpdateData();
                this.pulseSettingRepository.Save(this.pulseSettingListPresenter.PulseSettingList);

            }).AddTo(disposable);

            pulseSettingListPresenter.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                RegisterComponentPerSend();
            }).AddTo(disposable);
            
        }
        
        public void Initialize()
        {
            var data = pulseSettingRepository.Load();
            pulseSettingListPresenter.SetData(data);
            
            RegisterComponentPerSend();
        }

        public void Dispose()
        {
            disposable.Dispose();
        }

        private void RegisterComponentPerSend()
        {
            dispose.Dispose();
            dispose = new CompositeDisposable();
            
            foreach (var pulseSettingPresenter in pulseSettingListPresenter.PulseSettingPresenterList)
            {
                pulseSettingPresenter.OnSendButtonClickedAsObservable.Subscribe(__ =>
                {
                    sendToEndpointUseCase.Send(pulseSettingPresenter.PulseSetting.OscAddress, pulseSettingPresenter.PulseSetting.OscData);
                    
                }).AddTo(dispose);    // コンポーネント削除に対応できてないのでよくない -> IPulseSettingPresenterにDisposable持たせる
            }
            
        }
        
    }
    
}