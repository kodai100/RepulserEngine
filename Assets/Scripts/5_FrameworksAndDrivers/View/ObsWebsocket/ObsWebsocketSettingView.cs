using ProjectBlue.RepulserEngine.Controllers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class ObsWebsocketSettingView : MonoBehaviour
    {
        [Inject] private IObsWebsocketSettingController controller;
        [Inject] private IObsWebsocketCommunicationController communicationController;

        [SerializeField] private TMP_InputField serverAddressInput;
        [SerializeField] private TMP_InputField passwordInput;

        [SerializeField] private ObsWebsocketConnectButton connectButton;

        [SerializeField] private Toggle autoReconnectToggle;
        [SerializeField] private Toggle changeSceneToggle;
        [SerializeField] private Toggle restartMediaToggle;

        private bool connected;

        public void Start()
        {
            controller.Load();

            InitializeViewData();
            RegisterEvents();

            // auto reconnect procedure
            if (autoReconnectToggle.isOn)
            {
                Connect();
            }
        }

        private void InitializeViewData()
        {
            serverAddressInput.text = controller.ViewModel.ServerAddress.Value;
            passwordInput.text = controller.ViewModel.Password.Value;

            autoReconnectToggle.isOn = controller.ViewModel.AutoReconnectOnStart.Value;

            changeSceneToggle.isOn = controller.ViewModel.ChangeScene.Value;
            restartMediaToggle.isOn = controller.ViewModel.RestartMedia.Value;
        }

        private void RegisterEvents()
        {
            serverAddressInput.OnValueChangedAsObservable().Subscribe(value =>
            {
                // filter
                controller.ViewModel.ServerAddress.Value = value;
            }).AddTo(this);

            passwordInput.OnValueChangedAsObservable().Subscribe(value =>
            {
                controller.ViewModel.Password.Value = value;
            }).AddTo(this);

            autoReconnectToggle.OnValueChangedAsObservable().Subscribe(value =>
            {
                controller.ViewModel.AutoReconnectOnStart.Value = value;
            }).AddTo(this);

            changeSceneToggle.OnValueChangedAsObservable().Subscribe(value =>
            {
                controller.ViewModel.ChangeScene.Value = value;
            }).AddTo(this);

            restartMediaToggle.OnValueChangedAsObservable().Subscribe(value =>
            {
                controller.ViewModel.RestartMedia.Value = value;
            }).AddTo(this);

            connectButton.OnClicked.Subscribe(_ =>
            {
                controller.Save();

                if (!connected)
                {
                    Connect();
                }
                else
                {
                    Disconnect();
                }
            }).AddTo(this);
        }

        private void Connect()
        {
            connected = communicationController.Connect();

            if (connected)
            {
                connectButton.SetConnected();
            }
            else
            {
                connectButton.SetDisconnected();
            }
        }

        private void Disconnect()
        {
            communicationController.Disconnect();
            connectButton.SetDisconnected();

            connected = false;
        }
    }
}