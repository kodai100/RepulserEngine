using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{

    [Serializable]
    public class TabButtonAndWindowPair
    {
        [HideInInspector] public string name;
        public Button button;
        public CanvasGroup canvasGroup;

        public void Validate()
        {
            if (canvasGroup)
            {
                name = canvasGroup.name;
            }
        }
    }
    
    public class TabWindowView : MonoBehaviour
    {

        public List<TabButtonAndWindowPair> tabButtons;

        private void Start()
        {

            foreach (var buttons in tabButtons)
            {
                
                buttons.button.OnClickAsObservable().Subscribe(_ =>
                {
                    Display(buttons.canvasGroup);
                }).AddTo(this);
            }
            
            if (tabButtons.Count > 0)
            {
                Display(tabButtons[0].canvasGroup); // initial active
            }
            
        }

        private void OnValidate()
        {
            if(tabButtons.Count == 0) return;

            foreach (var buttons in tabButtons)
            {
                buttons.Validate();
            }
        }

        private void HideAll()
        {
            foreach (var buttons in tabButtons)
            {
                buttons.canvasGroup.interactable = false;
                buttons.canvasGroup.alpha = 0;
                buttons.canvasGroup.blocksRaycasts = false;
                
                var position = buttons.canvasGroup.transform.position;
                buttons.canvasGroup.transform.position = new Vector3(position.x, position.y, 0);
            }
        }

        public void Display(CanvasGroup canvasGroup)
        {
            HideAll();
            
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            
            var position = canvasGroup.transform.position;
            canvasGroup.transform.position = new Vector3(position.x, position.y, 1);
        }
    }

}

