using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.Presentation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{

    public class ConnectionCheckButton : MonoBehaviour
    {

        [Inject] private IConnectionCheckPresenter connectionCheckPresenter;

        private int index = -1;
        
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        
        [SerializeField] private Color successColor = new Color(0f, 0.7f, 0.3f);
        [SerializeField] private Color checkingColor = new Color(0.7f, 0.7f, 0f);
        [SerializeField] private Color failColor = new Color(0.7f, 0f, 0f);

        [SerializeField] private TextFade textFade;
        [SerializeField] private TMP_Text indexText;

        private bool running;

        public void SetIndex(int index)
        {
            this.index = index;
            image.color = failColor;

            if (indexText)
            {
                indexText.text = (index+1).ToString();
            }
        }
        
        private void Start()
        {
            
            textFade?.gameObject.SetActive(false);
            
            button.OnClickAsObservable().Subscribe(_ =>
            {
                Check();
            }).AddTo(this);
            
        }

        private async Task Check()
        {

            if (running || index == -1) return;
            
            running = true;

            image.color = checkingColor;
            
            textFade?.gameObject.SetActive(true);
            
            var result = await connectionCheckPresenter.Check(index);

            if (result)
            {
                image.color = successColor;
            }
            else
            {
                image.color = failColor;
            }
            
            textFade?.gameObject.SetActive(false);

            running = false;
        }

    }

}