using System;
using ProjectBlue.RepulserEngine.DataStore;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using Zenject;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{

    public class CommandSettingUseCase : IInitializable, IDisposable
    {

        private ICommandSettingListPresenter commandSettingListPresenter;
        private ICommandSettingRepository commandSettingRepository;
        
        private CompositeDisposable disposable = new CompositeDisposable();

        private Subject<OscMessage> onSendCommandSubject = new Subject<OscMessage>();
        public IObservable<OscMessage> OnSendCommandAsObservable => onSendCommandSubject;
        
        public CommandSettingUseCase(ICommandSettingListPresenter commandSettingListPresenter, ICommandSettingRepository commandSettingRepository)
        {
            this.commandSettingListPresenter = commandSettingListPresenter;
            this.commandSettingRepository = commandSettingRepository;

            commandSettingListPresenter.OnSaveAsObservable.Subscribe(list =>
            {
                // _CommandSettingListPresenter.UpdateData();
                this.commandSettingRepository.Save(list);

            }).AddTo(disposable);

        }
        
        public void Initialize()
        {
            var data = commandSettingRepository.Load();
            commandSettingListPresenter.SetData(data);
        }

        public void Dispose()
        {
            disposable.Dispose();
            onSendCommandSubject.Dispose();
        }

        
    }
    
}