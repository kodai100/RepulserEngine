using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    
    /// <summary>
    /// Reorderable list
    /// </summary>
    /// <typeparam name="T">List component with data model U (ReorderableListComponent<U>)</typeparam>
    /// <typeparam name="U">data model</typeparam>
    public abstract class ReorderableListView<T, U> : MonoBehaviour where T : ReorderableListComponentView<U>
    {

        [Inject] private DiContainer container;

        [SerializeField] private T listComponentPrefab;
        
        [SerializeField] private Button addButton;
        [SerializeField] protected Button saveButton;
        [SerializeField] private Button removeAllButton;
        [SerializeField] protected RectTransform scrollViewContentTransform;

        // bare component buffer (with no reordered)
        // リストのインデックスではなく、要素内に記録されているインデックスで参照するようにする
        private List<T> componentListBuffer = new List<T>();

        /// <summary>
        /// Reordered component list
        /// </summary>
        private IEnumerable<T> ComponentList =>
            componentListBuffer.OrderBy(component => component.Index);

        /// <summary>
        /// Reordered data list
        /// </summary>
        public List<U> DataList =>
            ComponentList.Select(component => component.Data).ToList();

        protected abstract void OnSaveButtonClicked(IEnumerable<U> items);
        
        /// <summary>
        /// リストが変更(再生成/追加/並び替え上下/クリア)されたときに継承先へ通知する
        /// </summary>
        /// <param name="items"></param>
        protected abstract void OnUpdateList(IEnumerable<U> items);
        
        protected virtual void Start()
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
                OnSaveButtonClicked(DataList);
                foreach (var component in ComponentList)
                {
                    component.SetBackgroundSaved();
                }
            }).AddTo(this);
            
        }

        // リスト追加ボタンが押されたときの処理
        private void AddToList()
        {
            // コンポーネントをインスタンス化
            var listComponent = InstantiateListComponent(listComponentPrefab);
            InitializeComponentEvents(listComponent);
            listComponent.Index = componentListBuffer.Count;    // 現在の最終インデックス+1を付与
            componentListBuffer.Add(listComponent);

            OnUpdateList(DataList);
        }

        private void ReorderUp(T listComponentView)
        {
            SwapIndex(listComponentView, GetBeforeListComponentOf(listComponentView));
            OnUpdateList(DataList);
        }
        
        
        private void ReorderDown(T listComponentView)
        {
            SwapIndex(listComponentView, GetAfterListComponentOf(listComponentView));
            OnUpdateList(DataList);
        }

        public void RecreateAllItem(IEnumerable<U> items)
        {
            // 既存のリストは削除
            ClearAll();
            
            foreach (var (item, index) in items.WithIndex())
            {
                var listComponent = InstantiateListComponent(listComponentPrefab);
                InitializeComponentEvents(listComponent);
                listComponent.UpdateView(item);
                listComponent.Index = index;
                componentListBuffer.Add(listComponent);
            }
            
            OnUpdateList(DataList);
        }

        private void Delete(T listComponent)
        {
            componentListBuffer.Remove(listComponent);
            Destroy(listComponent.gameObject);
                
            RecalculateIndex();
            OnUpdateList(DataList);
        }

        private T GetBeforeListComponentOf(T listComponent)
        {
            var index = listComponent.Index;
            if (index == 0)
            {
                throw new IndexOutOfRangeException("This component is first component.");
            }
            return ComponentList.FirstOrDefault(element => element.Index == index - 1);
        }
        
        private T GetAfterListComponentOf(T listComponent)
        {
            var index = listComponent.Index;
            if (index >= componentListBuffer.Count - 1)
            {
                throw new IndexOutOfRangeException("This component is last component.");
            }
            return ComponentList.FirstOrDefault(element => element.Index == index + 1);
        }

        private void SwapIndex(T target1, T target2)
        {
            var tmp = target1.Index;
            target1.Index = target2.Index;
            target2.Index = tmp;
            RearrangeGameObjects();
        }
        
        private void InitializeComponentEvents(T listComponentView)
        {
            listComponentView.Initialize(() =>
            {
                Delete(listComponentView);
            }, () =>
            {
                ReorderUp(listComponentView);
            }, () =>
            {
                ReorderDown(listComponentView);
            });
        }
        
        /// <summary>
        /// コンポーネントにDIしたい場合はここを拡張
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parentTransform"></param>
        /// <returns></returns>
        protected virtual T InstantiateListComponent(T prefab)
        {
            var obj = container.InstantiatePrefab(prefab, scrollViewContentTransform);
            var listComponent = obj.GetComponent<T>();
            return listComponent;
        }

        // if the component was deleted, should be recalculate indexes
        private void RecalculateIndex()
        {
            // コンポーネントをインデックス順に並び替え、昇順に振り直す
            foreach (var (component, i) in ComponentList.WithIndex())
            {
                component.Index = i;
            }
        }
        
        private void RearrangeGameObjects()
        {
            foreach (var item in ComponentList.Reverse())
            {
                item.transform.SetSiblingIndex(0);
            }
        }
        
        private void ClearAll()
        {
            componentListBuffer.ForEach(pulse =>
            {
                Destroy(pulse.gameObject);
            });
            componentListBuffer.Clear();

            OnUpdateList(DataList);
        }

    }

}
