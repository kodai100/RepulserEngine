using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.View;
using UnityEngine;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public abstract class ListPresenter<T, U, V> : MonoBehaviour where T : ListComponentPresenter<U, V> where U : ListComponentView<V>
    {

        [SerializeField] private ListView listView;
    
        [SerializeField] private T listComponentPrefab;

        [SerializeField, ReadOnly] public List<T> ComponentList = new List<T>();
        public IObservable<Unit> OnSaveButtonClickedAsObservable => listView.OnSaveButtonClickedAsObservable;
        
        private void Start()
        {
            listView.OnAddButtonClickedAsObservable.Subscribe(_ =>
            {
                AddToList();
            }).AddTo(this);

            listView.OnRemoveAllButtonClickedAsObservable.Subscribe(_ =>
            {
                ClearList();
            }).AddTo(this);

            StartInternal();
        }
        
        protected virtual void StartInternal() {}

        private void AddToList()
        {
            var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
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
        
        public void SetData(IEnumerable<V> data)
        {
            foreach (var component in data)
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    ComponentList.Remove(listComponentPresenter);
                });
                listComponentPresenter.SetData(component);
                ComponentList.Add(listComponentPresenter);
            }
        }
        
    }

}
