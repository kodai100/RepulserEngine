using System;
using ProjectBlue.RepulserEngine.View;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public abstract class ListComponentPresenter<T> : MonoBehaviour where T : ListComponentView
    {

        [SerializeField] protected T listComponentView;

        public void Initialize(Action onDeleteAction)
        {
            listComponentView.OnDeleteButtonClickedAsObservable.Subscribe(_ =>
            {
                onDeleteAction?.Invoke();
                Destroy(gameObject);
            }).AddTo(this);
        }

        public abstract void Load(int index);

        public abstract void Save(int index);

    }

}