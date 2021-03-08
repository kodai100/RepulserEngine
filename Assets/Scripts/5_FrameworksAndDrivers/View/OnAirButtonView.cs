using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine.View
{
    public class OnAirButtonView : MonoBehaviour
    {

        [SerializeField] private Button button;
        [SerializeField] private Image buttonBackground;

        [SerializeField] private TMP_Text text;

        [SerializeField] private Color onAirColor;
        [SerializeField] private Color standbyColor;

        private bool currentOnAir;
        
        private void Start()
        {
            button.OnClickAsObservable().Subscribe(_ =>
            {
                currentOnAir = !currentOnAir;

                if (currentOnAir)
                {
                    SetOnAir();
                }
                else
                {
                    SetStandby();
                }
                
            }).AddTo(this);
            
            SetStandby();
        }

        private void SetStandby()
        {
            buttonBackground.color = standbyColor;
            text.text = "STAND\nBY";
        }
        
        private void SetOnAir()
        {
            buttonBackground.color = onAirColor;
            text.text = "ON AIR";
        }
    }

}