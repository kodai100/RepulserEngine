using System;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.ViewModel
{
    public enum ObsConnectionCheckStatus
    {
        Connected,
        Failed,
        Checking
    }

    [Serializable]
    public class ObsWebsocketSettingViewModel
    {
        public ReactiveProperty<string> ServerAddress = new ReactiveProperty<string>();
        public ReactiveProperty<string> Password = new ReactiveProperty<string>();

        public ReactiveProperty<bool> AutoReconnectOnStart = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> ChangeScene = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> RestartMedia = new ReactiveProperty<bool>();

        public ObsWebsocketSettingViewModel(string serverAddress, string password, bool autoReconnectOnStart,
            bool changeScene, bool restartMedia)
        {
            ServerAddress.Value = serverAddress;
            Password.Value = password;
            AutoReconnectOnStart.Value = autoReconnectOnStart;
            ChangeScene.Value = changeScene;
            RestartMedia.Value = restartMedia;
        }

        public void Update(string serverAddress, string password, bool autoReconnectOnStart,
            bool changeScene, bool restartMedia)
        {
            ServerAddress.Value = serverAddress;
            Password.Value = password;
            AutoReconnectOnStart.Value = autoReconnectOnStart;
            ChangeScene.Value = changeScene;
            RestartMedia.Value = restartMedia;
        }

        public ObsWebsocketSettingViewModel() : this("", "", false, false, false)
        {
        }
    }
}