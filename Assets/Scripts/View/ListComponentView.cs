using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine
{
    public class ListComponentView : MonoBehaviour
    {
    
        [SerializeField] protected Button deleteButton;
    
        public IObservable<Unit> OnDeleteButtonClickedAsObservable => deleteButton.OnClickAsObservable();
    
    }

}