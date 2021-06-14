using ProjectBlue.RepulserEngine.Domain.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public class SignalSendAvailabilityButton : MonoBehaviour
    {
        private EndpointSettingViewModel endpointSettingviewModel;

        [SerializeField] private Button button;
        [SerializeField] private Image image;

        [SerializeField] private TMP_Text statusText;

        [SerializeField] private Color enabledColor = new Color(0f, 0.7f, 0.7f);
        [SerializeField] private Color disabledColor = new Color(0.7f, 0f, 0f);

        [SerializeField] private TMP_Text indexText;

        private void Start()
        {
            button.OnClickAsObservable().Subscribe(_ =>
            {
                endpointSettingviewModel.connectionEnabled.Value = !endpointSettingviewModel.ConnectionEnabled;
                ChangeBackground(endpointSettingviewModel.ConnectionEnabled);
            }).AddTo(this);
        }

        public void SetIndex(int index)
        {
            // image.color = failColor;

            if (indexText)
            {
                indexText.text = (index + 1).ToString();
            }
        }

        private void ChangeBackground(bool enabled)
        {
            image.color = enabled ? enabledColor : disabledColor;

            if (statusText)
            {
                statusText.text = enabled ? "Enabled" : "Disabled";
            }
        }

        // set with button recreation
        public void SetEndPointViewModel(EndpointSettingViewModel endpointSettingViewModel)
        {
            this.endpointSettingviewModel = endpointSettingViewModel;

            endpointSettingViewModel.connectionEnabled.Subscribe(ChangeBackground).AddTo(this);
        }
    }
}