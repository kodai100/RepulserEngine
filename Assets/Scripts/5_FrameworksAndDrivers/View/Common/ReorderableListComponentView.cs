using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine
{
    public abstract class ReorderableListComponentView<T> : MonoBehaviour
    {

        [SerializeField] protected TMP_Text indexText;
        [SerializeField] protected Button upButton;
        [SerializeField] protected Button deleteButton;
        [SerializeField] protected Button downButton;

        [SerializeField] protected Image backgroundImage;

        private int index = 0;
        private Color defaultBackground = new Color(0.145098f, 0.145098f, 0.145098f);
        
        private Color editedBackgroundColor = new Color(0.5f, 0.5f, 0f);
        private Color invalidBackgroundColor = new Color(0.5f, 0f, 0f);

        private void Awake()
        {
            defaultBackground = backgroundImage.color;
        }

        public int Index
        {
            get => index;
            set
            {
                index = value;
                OnUpdateIndex();
            }
        }

        public void Initialize(Action onDeleteAction, Action onUpAction, Action onDownAction)
        {
            deleteButton.OnClickAsObservable().Subscribe(_ =>
            {
                onDeleteAction?.Invoke();
            }).AddTo(this);

            upButton.OnClickAsObservable().Subscribe(_ =>
            {
                onUpAction?.Invoke();
            }).AddTo(this);

            downButton.OnClickAsObservable().Subscribe(_ =>
            {
                onDownAction?.Invoke();
            }).AddTo(this);
        }

        private void OnUpdateIndex()
        {
            indexText.text = $"{Index+1:D2}";
            OnUpdateIndex(Index);
        }

        protected virtual void OnUpdateIndex(int index)
        {
            
        }

        protected void SetDirty()
        {
            ChangeBackgroundColor(editedBackgroundColor);
        }
        
        protected void Invalid()
        {
            ChangeBackgroundColor(invalidBackgroundColor);
        }
        
        public void SetBackgroundSaved()
        {
            ChangeBackgroundColor(defaultBackground);
        }

        public void ChangeBackgroundColor(Color color)
        {
            backgroundImage.color = color;
        }

        public abstract void UpdateView(T data);
        public abstract T GetData();

    }

}