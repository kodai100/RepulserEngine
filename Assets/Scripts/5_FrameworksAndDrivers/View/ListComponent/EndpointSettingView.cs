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

                data = ParseData(ipTextField.text, portTextField.text);
                if (data == null)
                {
                    Invalid();
                }

            }).AddTo(this);
            
            connectionCheckButton.SetEndPointViewModel(data);
        }

        protected override void OnChangeIndex()
        {
            base.OnChangeIndex();
            connectionCheckButton.SetIndex(Index);
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

        private EndpointSettingViewModel ParseData(string ip, string port)
        {

            if (IPAddress.TryParse(ip, out var ipParsed) && int.TryParse(port, out var portParsed))
            {
                return new EndpointSettingViewModel(new IPEndPoint(ipParsed, portParsed), "", 0);
            }

            return null;
        }
    }
    
}