using System;
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

        public IObservable<string> OnIPValueChangedAsObservable => ipTextField.OnValueChangedAsObservable();
        public IObservable<string> OnPortValueChangedAsObservable => portTextField.OnValueChangedAsObservable();
        
        
        public override void SetData(EndpointSetting data)
        {
            ipTextField.text = data.EndPoint.Address.ToString();
            portTextField.text = data.EndPoint.Port.ToString();
            
            // TODO
        }
    }

}