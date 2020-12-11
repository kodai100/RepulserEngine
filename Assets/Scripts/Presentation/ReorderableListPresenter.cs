using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ProjectBlue.RepulserEngine
{
    public abstract class ReorderableListPresenter<T, U> : MonoBehaviour where T : ReorderableListComponentPresenter<U> where U : ReorderableListComponentView
    {

        [SerializeField] private ListView listView;
    
        [SerializeField] private T listComponentPrefab;

        [SerializeField, ReadOnly] protected List<T> ComponentList = new List<T>();
        
        protected abstract string SaveHash { get; }
        
        private void Start()
        {
            
            Load();
    
            listView.OnAddButtonClickedAsObservable.Subscribe(_ =>
            {
                AddToList();
            }).AddTo(this);
    
            listView.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                Save();
            }).AddTo(this);
    
            listView.OnRemoveAllButtonClickedAsObservable.Subscribe(_ =>
            {
                ClearList();
            }).AddTo(this);
    
        }

        private void AddToList()
        {
            var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
            listComponentPresenter.Initialize(() =>
            {
                ComponentList.Remove(listComponentPresenter);
            }, () =>
            {
                ReorderUp(listComponentPresenter);
            }, () =>
            {
                ReorderDown(listComponentPresenter);
            });
            ComponentList.Add(listComponentPresenter);
        }

        private void ReorderUp(T listComponentPresenter)
        {
            var currentIndex = listComponentPresenter.transform.GetSiblingIndex();
            listComponentPresenter.transform.SetSiblingIndex(Mathf.Clamp(currentIndex-1, 0, ComponentList.Count));
        }
        
        private void ReorderDown(T listComponentPresenter)
        {
            var currentIndex = listComponentPresenter.transform.GetSiblingIndex();
            listComponentPresenter.transform.SetSiblingIndex(Mathf.Clamp(currentIndex+1, 0, ComponentList.Count));
        }

        private void ClearList()
        {
            ComponentList.ForEach(pulse =>
            {
                Destroy(pulse.gameObject);
            });
            ComponentList.Clear();
        }
    
        private void Load()
        {
            var componentNum = PlayerPrefs.GetInt(SaveHash, 0);
            
            for (var i = 0; i < componentNum; i++)
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    ComponentList.Remove(listComponentPresenter);
                }, () =>
                {
                    ReorderUp(listComponentPresenter);
                }, () =>
                {
                    ReorderDown(listComponentPresenter);
                });
                listComponentPresenter.Load();
                ComponentList.Add(listComponentPresenter);
            }
        }
    
        private void Save()
        {
            ComponentList.ForEach(component =>
            {
                component.Save();
            });

            PlayerPrefs.SetInt(SaveHash, ComponentList.Count);

            Debug.Log("Saved");
        }
    }

}
