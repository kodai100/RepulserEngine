using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public abstract class ListComponentView<T> : MonoBehaviour
    {
    
        [SerializeField] protected Button deleteButton;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color defaultColor = Color.grey;
    
        public void Initialize(Action onDeleteAction)
        {
            deleteButton.OnClickAsObservable().Subscribe(_ =>
            {
                onDeleteAction?.Invoke();
                Destroy(gameObject);
            }).AddTo(this);
        }

        protected void SetDirty()
        {
            ChangeBackgroundColor(Color.red);
        }

        public void SetBackgroundSaved()
        {
            ChangeBackgroundColor(defaultColor);
        }
        
        public void ChangeBackgroundColor(Color color)
        {
            backgroundImage.color = color;
        }

        public abstract void UpdateView(T data);

    }

}