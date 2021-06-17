using System;
using OBSWebsocketDotNet;
using UniRx;
using Zenject;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Data.DataStore
{
    public class ObsWebsocketCommunicationDataStore : IObsWebsocketCommunicationDataStore, IDisposable
    {
        private OBSWebsocket obs = new OBSWebsocket();

        public IObservable<string> OnConnected => onConnected;
        public IObservable<string> OnDisconnected => onDisonnected;
        public IObservable<string> OnSceneChanged => onSceneChanged;

        private Subject<string> onConnected = new Subject<string>();
        private Subject<string> onDisonnected = new Subject<string>();
        private Subject<string> onSceneChanged = new Subject<string>();

        private string currentServerUrl = "";

        public ObsWebsocketCommunicationDataStore()
        {
            obs.Connected += (sender, e) => { onConnected.OnNext(obs.GetVersion().ToString()); };
            obs.Disconnected += (sender, e) => { onDisonnected.OnNext(currentServerUrl); };
            obs.SceneChanged += (sender, sceneName) => { onSceneChanged.OnNext(sceneName); };
        }

        // TODO: implement timeout
        public bool Connect(string serverAddress, string password)
        {
            if (!obs.IsConnected)
            {
                try
                {
                    obs.Connect(serverAddress, password);
                }
                catch (AuthFailureException)
                {
                    Debug.LogError("Authentication failed.");
                    return false;
                }
                catch (ErrorResponseException ex)
                {
                    Debug.LogError($"Connect failed : {ex.Message}");
                    return false;
                }

                currentServerUrl = serverAddress;

                if (obs.IsConnected)
                {
                    Debug.Log($"OBS Connected : {obs.GetVersion().OBSStudioVersion}");
                    return true;
                }

                return false;
            }


            Disconnect();
            return Connect(serverAddress, password);
        }

        public void Disconnect()
        {
            obs.Disconnect();
            Debug.Log($"OBS Disconnected : {currentServerUrl}");
        }

        public void SetScene(string sceneName)
        {
            if (obs.IsConnected)
            {
                obs.SetCurrentScene(sceneName);
            }
        }

        public void RestartMediaSource(string mediaSourceName)
        {
            if (obs.IsConnected)
            {
                obs.RestartMedia(mediaSourceName);
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }

    public class ObsWebsocketCommunicationDataStoreInstaller : Installer<ObsWebsocketCommunicationDataStoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObsWebsocketCommunicationDataStore>().AsSingle();
        }
    }
}