using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.View;
using UnityEngine;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public abstract class ReorderableListPresenter<T, U, V> : MonoBehaviour where T : ReorderableListComponentPresenter<U, V> where U : ReorderableListComponentView<V>
    {

        [SerializeField] protected ListView listView;
    
        [SerializeField] private T listComponentPrefab;

        [SerializeField, ReadOnly] private List<T> componentList = new List<T>();

        protected IEnumerable<T> ReorderedComponentList => componentList.OrderBy(component => component.Index);
        public IEnumerable<V> PulseSettingList => ReorderedComponentList.Select(presenter => presenter.Data);
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
                componentList.Remove(listComponentPresenter);
            }, () =>
            {
                ReorderUp(listComponentPresenter);
            }, () =>
            {
                ReorderDown(listComponentPresenter);
            });
            componentList.Add(listComponentPresenter);
        }

        private void ReorderUp(T listComponentPresenter)
        {
            var currentIndex = listComponentPresenter.transform.GetSiblingIndex();
            listComponentPresenter.transform.SetSiblingIndex(Mathf.Clamp(currentIndex-1, 0, componentList.Count));
        }
        
        private void ReorderDown(T listComponentPresenter)
        {
            var currentIndex = listComponentPresenter.transform.GetSiblingIndex();
            listComponentPresenter.transform.SetSiblingIndex(Mathf.Clamp(currentIndex+1, 0, componentList.Count));
        }

        private void ClearList()
        {
            componentList.ForEach(pulse =>
            {
                Destroy(pulse.gameObject);
            });
            componentList.Clear();
        }

        public void SetData(IEnumerable<V> data)
        {
            foreach (var component in data)
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    componentList.Remove(listComponentPresenter);
                }, () =>
                {
                    ReorderUp(listComponentPresenter);
                }, () =>
                {
                    ReorderDown(listComponentPresenter);
                });
                listComponentPresenter.SetData(component);
                componentList.Add(listComponentPresenter);
            }
        }

    }

}
