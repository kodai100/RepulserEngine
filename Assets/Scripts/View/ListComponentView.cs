using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public abstract class ListComponentView<T> : MonoBehaviour
    {
    
        [SerializeField] protected Button deleteButton;
    
        public IObservable<Unit> OnDeleteButtonClickedAsObservable => deleteButton.OnClickAsObservable();

        public abstract void SetData(T data);

    }

}