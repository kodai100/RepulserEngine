using ProjectBlue.RepulserEngine.Domain.DataModel;
using Zenject;

namespace ProjectBlue.RepulserEngine.Repository
{
    public class ObsWebsocketSettingRepository : IObsWebsocketSettingRepository
    {
        private bool loaded;

        private ObsWebsocketSettingDataModel cache;

        public void Save(ObsWebsocketSettingDataModel setting)
        {
            setting.ServerAddress = AesEncryption.Encrypt(setting.ServerAddress);
            setting.Password = AesEncryption.Encrypt(setting.Password);

            FileIOUtility.Write(setting, "ObsWebsocket");

            cache = setting;
        }

        public ObsWebsocketSettingDataModel Load()
        {
            if (loaded) return cache;

            var data = FileIOUtility.Read<ObsWebsocketSettingDataModel>("ObsWebsocket");

            data.ServerAddress = AesEncryption.Decrypt(data.ServerAddress);
            data.Password = AesEncryption.Decrypt(data.Password);

            loaded = true;

            cache = data;

            return data;
        }
    }

    public class ObsWebsocketSettingRepositoryInstaller : Installer<ObsWebsocketSettingRepositoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketSettingRepository>().AsSingle();
        }
    }
}