using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public class ListComponentView : MonoBehaviour
    {
    
        [SerializeField] protected Button deleteButton;
    
        public IObservable<Unit> OnDeleteButtonClickedAsObservable => deleteButton.OnClickAsObservable();
    
    }

}