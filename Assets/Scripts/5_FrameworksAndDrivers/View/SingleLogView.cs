using System;
using TMPro;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class SingleLogView : SingletonMonoBehaviour<LogListView>
    {

        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            UnityEngine.Application.logMessageReceived += OnLog;
        }

        private void Start()
        {
            Debug.Log("Initialized");
        }

        private void OnDestroy()
        {
            UnityEngine.Application.logMessageReceived -= OnLog;
        }

        private void OnLog(string logText, string stackTrace, LogType logType)
        {
            text.text = logText;

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