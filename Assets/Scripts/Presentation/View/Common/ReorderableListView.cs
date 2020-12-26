using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public abstract class ReorderableListView<T, U> : MonoBehaviour where T : ReorderableListComponentView<U>
    {

        [SerializeField] private T listComponentPrefab;
        
        [SerializeField] private Button addButton;
        [SerializeField] protected Button saveButton;
        [SerializeField] private Button removeAllButton;
        [SerializeField] protected RectTransform scrollViewParentTransform;

        [SerializeField, ReadOnly] private List<T> componentList = new List<T>();

        protected IEnumerable<T> ReorderedComponentList => componentList.OrderBy(component => component.Index);

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

        private void ReorderUp(T listComponentView)
        {
            var currentIndex = listComponentView.transform.GetSiblingIndex();
            listComponentView.transform.SetSiblingIndex(Mathf.Clamp(currentIndex-1, 0, componentList.Count));
            listComponentView.UpdateIndex();
        }
        
        private void ReorderDown(T listComponentView)
        {
            var currentIndex = listComponentView.transform.GetSiblingIndex();
            listComponentView.transform.SetSiblingIndex(Mathf.Clamp(currentIndex+1, 0, componentList.Count));
            listComponentView.UpdateIndex();
        }

        private void ClearList()
        {
            componentList.ForEach(pulse =>
            {
                Destroy(pulse.gameObject);
            });
            componentList.Clear();
        }

        public void SetData(IEnumerable<U> data)
        {
            foreach (var component in data)
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, scrollViewParentTransform);
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
                listComponentPresenter.UpdateView(component);
                componentList.Add(listComponentPresenter);
            }
        }

    }

}
