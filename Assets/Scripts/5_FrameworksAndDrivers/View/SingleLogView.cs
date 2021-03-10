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
        }
    }
    
}