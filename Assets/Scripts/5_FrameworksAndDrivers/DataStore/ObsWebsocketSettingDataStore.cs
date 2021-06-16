using System;
using System.IO;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using Zenject;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Data.DataStore
{
    public class ObsWebsocketSettingDataStore : IObsWebsocketSettingDataStore
    {
        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "ObsWebsocket.json");

        private bool loaded;

        private ObsWebsocketSettingDataModel cache;

        public void Save(ObsWebsocketSettingDataModel setting)
        {
            setting.ServerAddress = AesEncryption.Encrypt(setting.ServerAddress);
            setting.Password = AesEncryption.Encrypt(setting.Password);

            var json = JsonUtility.ToJson(setting);

            using (var sw = new StreamWriter(JsonFilePath, false))
            {
                try
                {
                    sw.Write(json);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            cache = setting;

            Debug.Log($"Saved : {JsonFilePath}");
        }

        public ObsWebsocketSettingDataModel Load()
        {
            if (loaded) return cache;

            var jsonDeserializedData = new ObsWebsocketSettingDataModel();

            try
            {
                using (var fs = new FileStream(JsonFilePath, FileMode.Open))
                using (var sr = new StreamReader(fs))
                {
                    var result = sr.ReadToEnd();

                    jsonDeserializedData = JsonUtility.FromJson<ObsWebsocketSettingDataModel>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log("No data");
                return jsonDeserializedData;
            }

            jsonDeserializedData.ServerAddress = AesEncryption.Decrypt(jsonDeserializedData.ServerAddress);
            jsonDeserializedData.Password = AesEncryption.Decrypt(jsonDeserializedData.Password);

            loaded = true;

            cache = jsonDeserializedData;

            return jsonDeserializedData;
        }
    }

    public class ObsWebsocketSettingDataStoreInstaller : Installer<ObsWebsocketSettingDataStoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketSettingDataStore>().AsSingle();
        }
    }
}