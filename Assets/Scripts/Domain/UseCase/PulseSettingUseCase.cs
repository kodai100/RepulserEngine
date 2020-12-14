
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
        private CompositeDisposable saveButtonRegistrationDisposable = new CompositeDisposable();

        public PulseSettingUseCase(SendToEndpointUseCase sendToEndpointUseCase, IPulseSettingListPresenter pulseSettingListPresenter, IPulseSettingRepository pulseSettingRepository)
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

        // Sendボタンを機能させる部分
        private void RegisterComponentPerSend()
        {
            // 永遠に残るのでセーブボタンが押されるたびに破棄して接続しなおす
            saveButtonRegistrationDisposable.Dispose();
            saveButtonRegistrationDisposable = new CompositeDisposable();
            
            foreach (var pulseSettingPresenter in pulseSettingListPresenter.PulseSettingPresenterList)
            {
                pulseSettingPresenter.OnSendButtonClickedAsObservable.Subscribe(__ =>
                {
                    var pulseSetting = pulseSettingPresenter.PulseSetting;
                    
                    if (String.IsNullOrEmpty(pulseSetting.OverrideIp))
                        sendToEndpointUseCase.Send(pulseSetting.OscAddress, pulseSetting.OscData);
                    else
                        sendToEndpointUseCase.SendToSpecificIP(pulseSetting.OscAddress, pulseSetting.OscData, pulseSetting.OverrideIp);
                    
                }).AddTo(saveButtonRegistrationDisposable);    // コンポーネント削除に対応できてないのでよくない -> IPulseSettingPresenterにDisposable持たせるのがベターだけど変な実装になるのでこうしている
            }
            
        }
        
    }
    
}