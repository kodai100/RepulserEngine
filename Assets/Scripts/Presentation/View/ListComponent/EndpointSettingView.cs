using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View.UniRx;
using TMPro;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class EndpointSettingView : ReorderableListComponentView<EndpointSetting>
    {

        [SerializeField] public TMP_InputField ipTextField;
        [SerializeField] public TMP_InputField portTextField;

        private void Start()
        {
            Observable.Merge(
                ipTextField.OnValueChangedAsObservable().Skip(1),
                portTextField.OnValueChangedAsObservable().Skip(1)
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
    

    namespace UniRx
    {
        public static class UnityUIExtensions
        {
            public static IObservable<string> OnValueChangedAsObservable(this TMP_InputField inputField)
            {
                return Observable.CreateWithState<string, TMP_InputField>(inputField, (i, observer) =>
                {
                    observer.OnNext(i.text);
                    return i.onValueChanged.AsObservable().Subscribe(observer);
                });
            }
        }
    }
}