using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Repository;
using ProjectBlue.RepulserEngine.Translators;
using Zenject;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class ObsWebsocketSettingUseCase : IObsWebsocketSettingUseCase
    {
        IObsWebsocketSettingRepository repository;
        public ObsWebsocketSettingViewModel ViewModel { get; } = new ObsWebsocketSettingViewModel();

        public ObsWebsocketSettingUseCase(IObsWebsocketSettingRepository repository)
        {
            this.repository = repository;
        }

        public void Save()
        {
            repository.Save(ObsWebsocketSettingTranslator.Translate(ViewModel));
        }

        public void Load()
        {
            var dataModel = repository.Load();

            ViewModel.Update(dataModel.ServerAddress, dataModel.Password,
                dataModel.AutoReconnectOnStart, dataModel.ChangeScene, dataModel.RestartMedia);
        }
    }

    public class ObsWebsocketSettingUseCaseInstaller : Installer<ObsWebsocketSettingUseCaseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketSettingUseCase>().AsSingle();
        }
    }
}