using System.Collections.Generic;
using ProjectBlue.RepulserEngine.View;
using UnityEngine;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public abstract class ListPresenter<T, U> : MonoBehaviour where T : ListComponentPresenter<U> where U : ListComponentView
    {

        [SerializeField] private ListView listView;
    
        [SerializeField] private T listComponentPrefab;

        [SerializeField, ReadOnly] public List<T> ComponentList = new List<T>();
        
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
    
        private void Load()
        {
            var componentNum = PlayerPrefs.GetInt(SaveHash, 0);
            
            for (var i = 0; i < componentNum; i++)
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    ComponentList.Remove(listComponentPresenter);
                });
                listComponentPresenter.Load(i);
                ComponentList.Add(listComponentPresenter);
            }
        }
    
        private void Save()
        {
            for (var i = 0; i < ComponentList.Count; i++)
            {
                ComponentList[i].Save(i);
            }
            
            PlayerPrefs.SetInt(SaveHash, ComponentList.Count);

            Debug.Log("Saved");
        }
    }

}
