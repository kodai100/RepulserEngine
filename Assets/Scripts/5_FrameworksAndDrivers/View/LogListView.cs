using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class LogListView : SingletonMonoBehaviour<LogListView>
    {
        
        [SerializeField] private RectTransform listComponentParentTransform;
        [SerializeField] private LogComponentView logComponentViewPrefab;

        private void Awake()
        {
            UnityEngine.Application.logMessageReceived += OnLog;
        }

        private void OnDestroy()
        {
            UnityEngine.Application.logMessageReceived -= OnLog;
        }

        private void OnLog(string logText, string stackTrace, LogType logType)
        {
            var obj = Instantiate(logComponentViewPrefab, listComponentParentTransform);
            obj.Log(logText);
        }
    }
    
}

