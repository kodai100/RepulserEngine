using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public class EndpointSettingView : ListComponentView<EndpointSetting>
    {

        [SerializeField] public InputField ipTextField;
        [SerializeField] public InputField portTextField;

        private void Start()
        {
            Observable.Merge(
                ipTextField.OnValueChangedAsObservable(),
                portTextField.OnValueChangedAsObservable()
            )
            .Subscribe(value =>
            {
                SetDirty();
            }).AddTo(this);
        }
        
        public override void UpdateView(EndpointSetting data)
        {
            ipTextField.text = data.EndPoint.Address.ToString();
            portTextField.text = data.EndPoint.Port.ToString();
        }
    }

}