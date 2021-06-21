using System;
using System.Collections.Generic;
using System.Text;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.Entity;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class MidiInputDisplayView : MonoBehaviour
    {
        [Inject] private IMidiCommunicationController midiController;

        [SerializeField] private TMP_Text text;
        [SerializeField] private Button clearButton;

        private List<string> stringList = new List<string>();
        private Queue<MidiData> queue = new Queue<MidiData>(28);

        private StringBuilder stringBuilder = new StringBuilder();

        private bool updated;

        private void Start()
        {
            midiController.OnMidiAsObservable.Subscribe(data => { queue.Enqueue(data); }).AddTo(this);

            clearButton?.OnClickAsObservable().Subscribe(_ => { stringList.Clear(); }).AddTo(this);
        }

        private void Update()
        {
            var time = DateTime.Now;

            while (queue.Count > 0)
            {
                var data = queue.Dequeue();
                stringList.Add(
                    $"[{time.Minute:D2}:{time.Second:D2}:{time.Millisecond:D3}] {data.midiType} ch:{data.Channel} n:{data.Number} v:{data.Value}");

                if (stringList.Count > 28)
                {
                    stringList.RemoveAt(0);
                }
            }

            stringBuilder.Clear();
            for (var i = stringList.Count - 1; i > 0; i--)
            {
                var component = stringList[i];
                stringBuilder.Append($"{component}\n");
            }

            text.text = stringBuilder.ToString();
        }
    }
}