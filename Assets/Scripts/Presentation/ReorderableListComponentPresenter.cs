using System;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public abstract class ReorderableListComponentPresenter<T, U> : MonoBehaviour where T : ReorderableListComponentView<U>
    {

        [SerializeField] protected T reorderableListComponentView;

        public int Index
        {
            get
            {
                return transform.GetSiblingIndex();
            }
        }

        public U Data;
        
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

        public void SetData(U data)
        {
            Data = data;
            reorderableListComponentView.SetData(data);
        }

        public abstract void UpdateData();

    }

}