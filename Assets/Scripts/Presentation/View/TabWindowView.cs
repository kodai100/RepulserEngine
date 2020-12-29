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

        private Color defaultColor;
        
        private void Start()
        {

            foreach (var buttons in tabButtons)
            {

                defaultColor = buttons.button.image.color;
                
                buttons.button.OnClickAsObservable().Subscribe(_ =>
                {
                    Display(buttons);
                }).AddTo(this);
            }
            
            if (tabButtons.Count > 0)
            {
                Display(tabButtons[0]); // initial active
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
                buttons.button.image.color = defaultColor;
                
                buttons.canvasGroup.interactable = false;
                buttons.canvasGroup.alpha = 0;
                buttons.canvasGroup.blocksRaycasts = false;
                
                var position = buttons.canvasGroup.transform.position;
                buttons.canvasGroup.transform.position = new Vector3(position.x, position.y, 0);
            }
        }

        public void Display(TabButtonAndWindowPair buttons)
        {
            HideAll();
            
            buttons.button.image.color = new Color(0.2f, 0.2f, 0.2f);
            
            buttons.canvasGroup.interactable = true;
            buttons.canvasGroup.alpha = 1;
            buttons.canvasGroup.blocksRaycasts = true;
            
            var position = buttons.canvasGroup.transform.position;
            buttons.canvasGroup.transform.position = new Vector3(position.x, position.y, 1);
        }
    }

}

