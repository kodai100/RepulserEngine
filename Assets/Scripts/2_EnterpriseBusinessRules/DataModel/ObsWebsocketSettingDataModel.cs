using System;

namespace ProjectBlue.RepulserEngine.Domain.DataModel
{
    [Serializable]
    public class ObsWebsocketSettingDataModel
    {
        public string ServerAddress;
        public string Password; // TODO : Security management, currently saved as plaintext ...

        public bool AutoReconnectOnStart;
        public bool ChangeScene;
        public bool RestartMedia;

        public ObsWebsocketSettingDataModel(string serverAddress, string password, bool autoReconnectOnStart,
            bool changeScene, bool restartMedia)
        {
            ServerAddress = serverAddress;
            Password = password;
            AutoReconnectOnStart = autoReconnectOnStart;
            ChangeScene = changeScene;
            RestartMedia = restartMedia;
        }

        public ObsWebsocketSettingDataModel() : this("", "", false, false, false)
        {
        }
    }
}