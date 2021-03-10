using TMPro;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{

    public class TextFade : MonoBehaviour
    {

        [SerializeField] private TMP_Text text;

        [SerializeField] private float speed = 1;
        
        private Color defaultTextColor;

        private float time;
        private float currentAlpha;
        
        private void Start()
        {
            defaultTextColor = text.color;
        }
        
        private void Update()
        {

            time += Time.deltaTime;

            currentAlpha = Mathf.Sin(time * speed * 10) * 0.5f + 0.5f;

            defaultTextColor.a = currentAlpha;
            text.color = defaultTextColor;

        }
    }

}