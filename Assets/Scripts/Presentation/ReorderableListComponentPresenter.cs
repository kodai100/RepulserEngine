using System;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    public abstract class ReorderableListComponentPresenter<T> : MonoBehaviour where T : ReorderableListComponentView
    {

        [SerializeField] protected T reorderableListComponentView;

        protected int Index => transform.GetSiblingIndex();
        
        public void Initialize(Action onDeleteAction, Action onUpAction, Action onDownAction)
        {
            reorderableListComponentView.OnDeleteButtonClickedAsObservable.Subscribe(_ =>
            {
                onDeleteAction?.Invoke();
                Destroy(gameObject);
            }).AddTo(this);

            reorderableListComponentView.OnUpButtonClickedAsObservable.Subscribe(_ =>
            {
                onUpAction?.Invoke();
            }).AddTo(this);

            reorderableListComponentView.OnDownButtonClickedAsObservable.Subscribe(_ =>
            {
                onDownAction?.Invoke();
            }).AddTo(this);
        }

        public abstract void Load();

        public abstract void Save();

    }

}