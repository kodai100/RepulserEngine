using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using UnityEngine;
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

            try
            {
                data.ServerAddress = AesEncryption.Decrypt(data.ServerAddress);
                data.Password = AesEncryption.Decrypt(data.Password);
            }
            catch (Exception e)
            {
                Debug.LogError("Encryption info is updated. please re-enter address and password.");
                data.ServerAddress = "";
                data.Password = "";
            }

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