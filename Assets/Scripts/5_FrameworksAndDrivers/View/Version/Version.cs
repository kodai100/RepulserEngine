using TMPro;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class Version : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            text.text = $"RepulserEngine v{UnityEngine.Application.version}";
        }
    }
}