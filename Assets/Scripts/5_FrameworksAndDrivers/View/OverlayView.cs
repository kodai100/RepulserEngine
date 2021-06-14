using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.ViewInterfaces;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class OverlayView : MonoBehaviour, IOverlayView
    {
        [Inject] private IOverlayController _overlayController;

        [SerializeField] private Image overlayImage;
        [SerializeField] private float fadeTime = 1;

        [SerializeField] private Color warningColor = Color.red;
        [SerializeField] private float defaultAlpha = 0.5f;

        private Color currentColor;

        private float time = 0;

        private float fraction = 0;

        private void Start()
        {
            Finish();

            _overlayController.OnOverlayTriggeredAsObservable.Subscribe(color => { Trigger(); }).AddTo(this);
        }

        public void Trigger()
        {
            currentColor = new Color(warningColor.r, warningColor.g, warningColor.b, defaultAlpha);
            overlayImage.enabled = true;
            time = 0;
        }

        private void Finish()
        {
            overlayImage.enabled = false;
        }

        private void Update()
        {
            time += Time.deltaTime;

            fraction = time / fadeTime;

            currentColor.a = (1 - fraction) * defaultAlpha;
            overlayImage.color = currentColor;

            if (time > fadeTime)
            {
                Finish();
            }
        }
    }
}