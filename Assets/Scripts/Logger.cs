using UnityEngine;
using UnityEngine.UI;

namespace ProjectBlue.RepulserEngine
{
    public class Logger : SingletonMonoBehaviour<Logger>
    {

        [SerializeField] private Text logText;

        public void Log(string text)
        {
            logText.text = text;
            Debug.Log(text);
        }
    }
    
}

