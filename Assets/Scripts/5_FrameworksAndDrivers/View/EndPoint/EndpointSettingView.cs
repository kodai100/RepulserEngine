using System.Net;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class EndpointSettingView : ReorderableListComponentView<EndpointSettingViewModel>
    {

        [SerializeField] public TMP_InputField ipTextField;
        [SerializeField] public TMP_InputField portTextField;

        [SerializeField] private ConnectionCheckButton connectionCheckButton;
        [SerializeField] private SignalSendAvailabilityButton signalSendAvailabilityButton;

        public override EndpointSettingViewModel Data => data;
        private EndpointSettingViewModel data = new EndpointSettingViewModel();

        private void Start()
        {
            Observable.Merge(
                ipTextField.OnValueChangedAsObservable().Skip(1),
                portTextField.OnValueChangedAsObservable().Skip(1)
                )
            .Subscribe(value =>
            {
                SetDirty();
                
                if (ParseData(ipTextField.text, portTextField.text))
                {
                    Invalid();
                }

            }).AddTo(this);
            
            connectionCheckButton.SetEndPointViewModel(data);
            signalSendAvailabilityButton.SetEndPointViewModel(data);
        }

        protected override void OnChangeIndex()
        {
            base.OnChangeIndex();
            connectionCheckButton.SetIndex(Index);
            signalSendAvailabilityButton.SetEndPointViewModel(data);
        }

        public override void UpdateView(EndpointSettingViewModel data)
        {
            if (data == null)
            {
                ipTextField.text = "";
                portTextField.text = "";
                return;
            }
            
            this.data = data;
            ipTextField.text = data.EndPoint.Address.ToString();
            portTextField.text = data.EndPoint.Port.ToString();
        }

        private bool ParseData(string ip, string port)
        {

            if (IPAddress.TryParse(ip, out var ipParsed) && int.TryParse(port, out var portParsed))
            {
                data.ip.Value = ipParsed.ToString();
                data.port.Value = portParsed;
                return true;
            }

            return false;
        }
    }
    
}