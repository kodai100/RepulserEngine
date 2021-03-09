using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Infrastructure;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public abstract class ReorderableListView<T, U> : MonoBehaviour where T : ReorderableListComponentView<U>
    {

        [Inject] private DiContainer container;
        
        [SerializeField] private T listComponentPrefab;
        
        [SerializeField] private Button addButton;
        [SerializeField] protected Button saveButton;
        [SerializeField] private Button removeAllButton;
        [SerializeField] protected RectTransform scrollViewParentTransform;

        // リストのインデックスではなく、要素内に記録されているインデックスで参照するようにする
        [SerializeField, ReadOnly] private List<T> componentList = new List<T>();

        protected abstract void OnSaveButtonClicked(IEnumerable<U> items);
        protected abstract void OnUpdateList(IEnumerable<U> items);
        
        private void Start()
        {

            // リスト追加ボタンが押されたら
            addButton.OnClickAsObservable().Subscribe(_ =>
            {
                AddToList();
            }).AddTo(this);
            
            // 全削除ボタンが押されたら
            removeAllButton.OnClickAsObservable().Subscribe(_ =>
            {
                ClearAll();
            }).AddTo(this);
            
            // 保存ボタンが押されたら
            saveButton.OnClickAsObservable().Subscribe(_ =>
            {
                OnSaveButtonClicked(componentList.Select(componentView => componentView.GetData()));
                foreach (var component in componentList)
                {
                    component.SetBackgroundSaved();
                }
            }).AddTo(this);

            StartInternal();
        }

        protected abstract void StartInternal();

        // リスト追加ボタンが押されたときの処理
        private void AddToList()
        {
            // コンポーネントをインスタンス化
            var obj = container.InstantiatePrefab(listComponentPrefab, scrollViewParentTransform);
            var listComponentView = obj.GetComponent<T>();
            InitializeComponent(listComponentView);
            listComponentView.Index = componentList.Count;
            componentList.Add(listComponentView);

            OnUpdateList();
        }

        
        private void ReorderUp(T listComponentView)
        {
            SwapIndex(listComponentView, GetBeforeListComponentOf(listComponentView));
            OnUpdateList();
        }
        
        
        private void ReorderDown(T listComponentView)
        {
            SwapIndex(listComponentView, GetAfterListComponentOf(listComponentView));
            OnUpdateList();
        }

        public void RecreateAllItem(IEnumerable<U> items)
        {
            componentList.Clear();
            
            foreach (var (item, index) in items.WithIndex())
            {
                var obj = container.InstantiatePrefab(listComponentPrefab, scrollViewParentTransform);
                var listComponentView = obj.GetComponent<T>();
                InitializeComponent(listComponentView);
                listComponentView.UpdateView(item);
                listComponentView.Index = index;
                componentList.Add(listComponentView);
            }
            
            OnUpdateList();
        }

        private T GetBeforeListComponentOf(T listComponentView)
        {
            var index = listComponentView.Index;
            if (index == 0)
            {
                throw new IndexOutOfRangeException("This component is first component.");
            }
            return componentList.FirstOrDefault(element => element.Index == index-1);
        }
        
        private T GetAfterListComponentOf(T listComponentView)
        {
            var index = listComponentView.Index;
            if (index >= componentList.Count - 1)
            {
                throw new IndexOutOfRangeException("This component is last component.");
            }
            return componentList.FirstOrDefault(element => element.Index == index+1);
        }

        private void SwapIndex(T target1, T target2)
        {
            var tmp = target1.Index;
            target1.Index = target2.Index;
            target2.Index = tmp;
            RearrangeGameObjects();
        }
        
        private void InitializeComponent(T listComponentView)
        {
            listComponentView.Initialize(() =>
            {
                componentList.Remove(listComponentView);
                Destroy(listComponentView.gameObject);

                // TODO: インデックスの再計算
                foreach (var (component, i) in componentList.OrderBy(element => element.Index).WithIndex())
                {
                    component.Index = i;
                }
            }, () =>
            {
                ReorderUp(listComponentView);
            }, () =>
            {
                ReorderDown(listComponentView);
            });
        }

        private void RearrangeGameObjects()
        {
            foreach (var item in componentList.OrderBy(element => element.Index).Reverse())
            {
                item.transform.SetSiblingIndex(0);
            }
        }
        
        private void ClearAll()
        {
            componentList.ForEach(pulse =>
            {
                Destroy(pulse.gameObject);
            });
            componentList.Clear();

            OnUpdateList();
        }

        private void OnUpdateList()
        {
            OnUpdateList(componentList.OrderBy(element => element.Index).Select(componentView => componentView.GetData()));
        }

    }

}
