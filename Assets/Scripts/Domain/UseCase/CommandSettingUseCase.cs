
using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class CommandSettingUseCase : IInitializable, IDisposable
    {

        private ICommandSettingListPresenter CommandSettingListPresenter;
        private ICommandSettingRepository CommandSettingRepository;
        
        private CompositeDisposable disposable = new CompositeDisposable();
        private CompositeDisposable saveButtonRegistrationDisposable = new CompositeDisposable();

        private Subject<OscMessage> onSendCommandSubject = new Subject<OscMessage>();
        public IObservable<OscMessage> OnSendCommandAsObservable => onSendCommandSubject;
        
        public CommandSettingUseCase(ICommandSettingListPresenter CommandSettingListPresenter, ICommandSettingRepository CommandSettingRepository)
        {
            this.CommandSettingListPresenter = CommandSettingListPresenter;
            this.CommandSettingRepository = CommandSettingRepository;

            this.CommandSettingListPresenter.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                // _CommandSettingListPresenter.UpdateData();
                this.CommandSettingRepository.Save(this.CommandSettingListPresenter.CommandSettingList);

            }).AddTo(disposable);

            CommandSettingListPresenter.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                RegisterComponentPerSend("send");
            }).AddTo(disposable);
            
        }
        
        public void Initialize()
        {
            var data = CommandSettingRepository.Load();
            CommandSettingListPresenter.SetData(data);
            
            RegisterComponentPerSend("");
        }

        public void Dispose()
        {
            disposable.Dispose();
            onSendCommandSubject.Dispose();
        }

        // Sendボタンを機能させる部分
        private void RegisterComponentPerSend(string command)
        {
            
            // 永遠に残るのでセーブボタンが押されるたびに破棄して接続しなおす
            saveButtonRegistrationDisposable.Dispose();
            saveButtonRegistrationDisposable = new CompositeDisposable();
            
            foreach (var commandSettingPresenter in CommandSettingListPresenter.CommandSettingPresenterList)
            {
                
                commandSettingPresenter.OnPauseButtonClickedAsObservable.Subscribe(__ =>
                {
                    var commandSetting = commandSettingPresenter.CommandSetting;
                    var commandType = commandSetting.OscAddress + "_" + "PAUSE";
                    onSendCommandSubject.OnNext(new OscMessage(commandType, ""));
                }).AddTo(saveButtonRegistrationDisposable);    // コンポーネント削除に対応できてないのでよくない -> ICommandSettingPresenterにDisposable持たせるのがベターだけど変な実装になるのでこうしている
                
                commandSettingPresenter.OnStopButtonClickedAsObservable.Subscribe(__ =>
                {
                    var commandSetting = commandSettingPresenter.CommandSetting;
                    var commandType = commandSetting.OscAddress + "_" + "STOP";
                    onSendCommandSubject.OnNext(new OscMessage(commandType, ""));
                }).AddTo(saveButtonRegistrationDisposable);  

                commandSettingPresenter.OnBackButtonClickedAsObservable.Subscribe(__ =>
                {
                    var commandSetting = commandSettingPresenter.CommandSetting;
                    var commandType = commandSetting.OscAddress + "_" + "BACK";
                    onSendCommandSubject.OnNext(new OscMessage(commandType, ""));
                }).AddTo(saveButtonRegistrationDisposable);  

                commandSettingPresenter.OnSkipButtonClickedAsObservable.Subscribe(__ =>
                {
                    var commandSetting = commandSettingPresenter.CommandSetting;
                    var commandType = commandSetting.OscAddress + "_" + "SKIP";
                    onSendCommandSubject.OnNext(new OscMessage(commandType, ""));
                }).AddTo(saveButtonRegistrationDisposable);  
            }

            
        }
        
    }
    
}