using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ProjectBlue.RepulserEngine
{
    public abstract class ListPresenter<T, U> : MonoBehaviour where T : ListComponentPresenter<U> where U : ListComponentView
    {

        [SerializeField] private ListView listView;
    
        [SerializeField] private T listComponentPrefab;

        protected readonly List<T> ComponentList = new List<T>();
        
        protected abstract string SaveHash { get; }
        
        private void Start()
        {
            
            Load();
    
            listView.OnAddButtonClickedAsObservable.Subscribe(_ =>
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    ComponentList.Remove(listComponentPresenter);
                });
                ComponentList.Add(listComponentPresenter);
            }).AddTo(this);
    
            listView.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                Save();
            }).AddTo(this);
    
            listView.OnRemoveAllButtonClickedAsObservable.Subscribe(_ =>
            {
                ComponentList.ForEach(pulse =>
                {
                    Destroy(pulse.gameObject);
                });
                ComponentList.Clear();
                
            }).AddTo(this);
    
        }
    
        private void Load()
        {
            var componentNum = PlayerPrefs.GetInt(SaveHash, 0);
            
            for (var i = 0; i < componentNum; i++)
            {
                var listComponentPresenter = Instantiate(listComponentPrefab, listView.ScrollViewParentTransform);
                listComponentPresenter.Initialize(() =>
                {
                    ComponentList.Remove(listComponentPrefab);
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
        }
    }

}
