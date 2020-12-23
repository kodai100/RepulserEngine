using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public abstract class ListView<T, U> : MonoBehaviour where T : ListComponentView<U>
    {

        [SerializeField] private Button addButton;
        [SerializeField] protected Button saveButton;
        [SerializeField] private Button removeAllButton;
        [SerializeField] protected RectTransform scrollViewParentTransform;
        
        [SerializeField] private T listComponentPrefab;

        [SerializeField, ReadOnly] public List<T> ComponentList = new List<T>();

        private void Start()
        {
            addButton.OnClickAsObservable().Subscribe(_ =>
            {
                AddToList();
            }).AddTo(this);

            removeAllButton.OnClickAsObservable().Subscribe(_ =>
            {
                ClearList();
            }).AddTo(this);

            StartInternal();
        }
        
        protected virtual void StartInternal() {}

        private void AddToList()
        {
            var listComponentPresenter = Instantiate(listComponentPrefab, scrollViewParentTransform);
            
            listComponentPresenter.Initialize(() =>
            {
                ComponentList.Remove(listComponentPresenter);
            });
            ComponentList.Add(listComponentPresenter);
        }

        private void ClearList()
        {
            ComponentList.ForEach(pulse =>
            {
                Destroy(pulse.gameObject);
            });
            ComponentList.Clear();
        }
        
        public void SetData(IEnumerable<U> data)
        {
            
            foreach (var component in data)
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, scrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    ComponentList.Remove(listComponentPresenter);
                });
                listComponentPresenter.UpdateView(component);
                ComponentList.Add(listComponentPresenter);
            }
            
        }
        
    }

}
