using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
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
        private int prevComponentCount = 0;

        protected abstract void OnSaveButtonClicked(IEnumerable<U> items);
        
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
            
            saveButton.OnClickAsObservable().Subscribe(_ =>
            {
                OnSaveButtonClicked(componentList.Select(componentView => componentView.GetData()));
                foreach (var component in componentList)
                {
                    component.SetBackgroundSaved();
                }
            }).AddTo(this);

            this.UpdateAsObservable().Subscribe(_ =>
            {
                if (componentList.Count != prevComponentCount)
                {
                    RecalculateIndex();
                    prevComponentCount = componentList.Count;
                }
            }).AddTo(this);
            
            StartInternal();
        }

        protected abstract void StartInternal();

        private void AddToList()
        {
            var listComponentPresenter = Instantiate(listComponentPrefab, scrollViewParentTransform);
            listComponentPresenter.Initialize(() =>
            {
                componentList.Remove(listComponentPresenter);
                Destroy(listComponentPresenter.gameObject);
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
            var currentIndex = listComponentView.Index;
            SwapComponent(currentIndex,currentIndex-1);
        }
        
        private void ReorderDown(T listComponentView)
        {
            var currentIndex = listComponentView.Index;
            SwapComponent(currentIndex,currentIndex+1);
        }

        private void SwapComponent(int oldIndex, int newIndex)
        {
            if (newIndex < 0 || componentList.Count <= newIndex) return;

            var dataCache = componentList[oldIndex].GetData();
            componentList[oldIndex].UpdateView(componentList[newIndex].GetData());
            componentList[newIndex].UpdateView(dataCache);
        }

        private void ClearList()
        {
            componentList.ForEach(pulse =>
            {
                Destroy(pulse.gameObject);
            });
            componentList.Clear();
        }

        public void SetData(IEnumerable<U> items)
        {
            componentList.Clear();
            
            foreach (var (item, index) in items.Select((item, index) => (item, index)))
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, scrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    componentList.Remove(listComponentPresenter);
                    Destroy(listComponentPresenter.gameObject);
                }, () =>
                {
                    ReorderUp(listComponentPresenter);
                }, () =>
                {
                    ReorderDown(listComponentPresenter);
                });
                listComponentPresenter.UpdateView(item);
                listComponentPresenter.Index = index;
                componentList.Add(listComponentPresenter);
            }
        }

        private void RecalculateIndex()
        {
            foreach (var (item, index) in componentList.Select((item, index) => (item, index)))
            {
                item.Index = index;
            }
        }

    }

}
