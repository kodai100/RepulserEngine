using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public class ObsWebsocketConnectButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        [SerializeField] private Image backgroundImage;
        [SerializeField] private TMP_Text label;

        [SerializeField] private Color connectedColor = Color.cyan;

        private Color disconnectedColor;

        public IObservable<Unit> OnClicked => button.OnClickAsObservable();

        private void Start()
        {
            disconnectedColor = backgroundImage.color;
        }

        public void SetConnected()
        {
            label.text = "Disconnect";
            backgroundImage.color = connectedColor;
        }

        public void SetDisconnected()
        {
            label.text = "Connect";
            backgroundImage.color = disconnectedColor;
        }
    }
}