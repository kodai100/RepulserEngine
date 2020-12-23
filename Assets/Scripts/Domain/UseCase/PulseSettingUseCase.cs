
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
            onSendTriggeredSubject.Dispose();
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
                    
                    onSendTriggeredSubject.OnNext(new OscMessage(pulseSetting.OverrideIp, pulseSetting.OscAddress, pulseSetting.OscData));
                    
                }).AddTo(saveButtonRegistrationDisposable);    // コンポーネント削除に対応できてないのでよくない -> IPulseSettingPresenterにDisposable持たせるのがベターだけど変な実装になるのでこうしている
            }
            
        }
        
    }
    
}