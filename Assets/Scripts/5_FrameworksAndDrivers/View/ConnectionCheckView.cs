using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{

    public class ConnectionCheckView : MonoBehaviour
    {

        [Inject] private IConnectionCheckPresenter connectionCheckPresenter;

        [SerializeField] private Button button;
        [SerializeField] private EndpointSettingView endpointSettingView;
        [SerializeField] private Image image;
        
        [SerializeField] private Color successColor = new Color(0f, 0.7f, 0.3f);
        [SerializeField] private Color failColor = new Color(0.7f, 0f, 0f);

        [SerializeField] private TextFade textFade;
        
        private void Start()
        {
            
            textFade.gameObject.SetActive(false);
            
            button.OnClickAsObservable().Subscribe(_ =>
            {
                Check();
            }).AddTo(this);
            
        }

        private async Task Check()
        {

            if (endpointSettingView.GetData() == null)
            {
                image.color = failColor;
                return;
            }
            
            textFade.gameObject.SetActive(true);
            
            var address = endpointSettingView.GetData().EndPoint.Address;
            Debug.Log($"Check : {address}");
            
            var result = await connectionCheckPresenter.Check(address);

            if (result)
            {
                image.color = successColor;
                Debug.Log($"Success : {address}");
            }
            else
            {
                image.color = failColor;
                Debug.Log($"Fail : {address}");
            }
            textFade.gameObject.SetActive(false);
        }

    }

}