using System;
using TMPro;

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

        public static IObservable<int> OnValueChangedAsObservable(this TMP_Dropdown dropdown)
        {
            return Observable.CreateWithState<int, TMP_Dropdown>(dropdown, (i, observer) =>
            {
                observer.OnNext(i.value);
                return i.onValueChanged.AsObservable().Subscribe(observer);
            });
        }
    }
}