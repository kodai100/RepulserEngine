using System;
using ProjectBlue.RepulserEngine.View;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public abstract class ListComponentPresenter<T, U> : MonoBehaviour where T : ListComponentView<U>
    {

        [SerializeField] protected T listComponentView;

        public U Data;
        
        public void Initialize(Action onDeleteAction)
        {
            listComponentView.OnDeleteButtonClickedAsObservable.Subscribe(_ =>
            {
                onDeleteAction?.Invoke();
                Destroy(gameObject);
            }).AddTo(this);
        }
        
        public void SetData(U data)
        {
            Data = data;
            listComponentView.SetData(data);
        }

        public abstract void UpdateData();

    }

}