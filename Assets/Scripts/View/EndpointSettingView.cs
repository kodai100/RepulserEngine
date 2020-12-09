using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine
{
    public class EndpointSettingView : ListComponentView
    {

        [SerializeField] private InputField ipTextField;
        [SerializeField] private InputField portTextField;

        public string IP => ipTextField.text;
        public string Port => portTextField.text;

        public IObservable<string> OnIPValueChangedAsObservable => ipTextField.OnValueChangedAsObservable();
        public IObservable<string> OnPortValueChangedAsObservable => portTextField.OnValueChangedAsObservable();

        public void SetIPText(string ip)
        {
            ipTextField.text = ip;
        }

        public void SetPortText(int port)
        {
            ipTextField.text = port.ToString();
        }


    }

}