using TMPro;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class LogComponentView : MonoBehaviour
    {

        [SerializeField] private TMP_Text text;
        
        public void Log(string log)
        {
            text.text = log;
        }
        
    }

}

