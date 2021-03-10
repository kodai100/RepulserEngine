using TMPro;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    
    public class LogComponentView : MonoBehaviour
    {

        [SerializeField] private TMP_Text text;
        
        public void Log(string log, LogType logType)
        {
            text.text = log;

            switch (logType)
            {
                case LogType.Log:
                    text.color = Color.white;
                    break;
                case LogType.Warning:
                    text.color = Color.yellow;
                    break;
                case LogType.Error:
                case LogType.Exception:
                case LogType.Assert:
                    text.color = Color.red;
                    break;
                default:
                    text.color = Color.white;
                    break;
            }
        }
        
    }

}

