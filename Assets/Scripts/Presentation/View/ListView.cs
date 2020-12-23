using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public class ListView : MonoBehaviour
    {
        [SerializeField] private Button addButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button removeAllButton;
        [SerializeField] protected RectTransform scrollViewParentTransform;

        public IObservable<Unit> OnAddButtonClickedAsObservable => addButton.OnClickAsObservable();
        public IObservable<Unit> OnSaveButtonClickedAsObservable => saveButton.OnClickAsObservable();
        public IObservable<Unit> OnRemoveAllButtonClickedAsObservable => removeAllButton.OnClickAsObservable();

        public RectTransform ScrollViewParentTransform => scrollViewParentTransform;
    }
    
}