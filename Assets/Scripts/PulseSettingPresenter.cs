using System;
using Ltc;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    public class PulseSettingPresenter : MonoBehaviour
    {
        // TODO clean architecture
        [SerializeField] private PulseSettingView _pulseSettingView;

        private bool alreadyPulsed = false;

        private PulseSetting pulseSetting = null;

        public void Initialize(Action onDeleteAction)
        {
            _pulseSettingView.OnDeleteButtonClickedAsObservable.Subscribe(_ =>
            {
                onDeleteAction?.Invoke();
                Destroy(gameObject);
            }).AddTo(this);
        }

        private void Start()
        {
            Observable.Merge(
                _pulseSettingView.OscAddressAsObservable,
                _pulseSettingView.OscDataAsObservable,
                _pulseSettingView.HourAsObservable,
                _pulseSettingView.MinuteAsObservable,
                _pulseSettingView.SecondAsObservable,
                _pulseSettingView.FrameAsObservable
            ).Subscribe(value =>
            {
                _pulseSettingView.SetEdited();
            }).AddTo(this);
        }

        public void Evaluate(Timecode timecode)
        {
            if (pulseSetting == null) return;

            if (timecode < pulseSetting.Timecode)
            {
                alreadyPulsed = false;
                _pulseSettingView.SetBefore();
            }
            
            if (timecode == pulseSetting.Timecode)
            {
                _pulseSettingView.SetPulse();
                Pulse();
            }
            
            if (pulseSetting.Timecode < timecode)
            {
                _pulseSettingView.SetAfter();
            }
        }

        public void Load(int index)
        {
            pulseSetting = PulseSetting.Load(index);
            _pulseSettingView.SetData(pulseSetting);
        }
        
        public void Save(int index)
        {
            pulseSetting = new PulseSetting(
                index,
                _pulseSettingView.oscAddressField.text,
                _pulseSettingView.oscDataField.text,
                new Timecode
                {
                    dropFrame = false,
                    frame = int.Parse(_pulseSettingView.timecodeFrameInputField.text),
                    hour = int.Parse(_pulseSettingView.timecodeHourInputField.text),
                    minute = int.Parse(_pulseSettingView.timecodeMinuteInputField.text),
                    second = int.Parse(_pulseSettingView.timecodeSecondInputField.text),
                }
            );
            
            pulseSetting.Save();
            _pulseSettingView.SetSaved();
        }
        
        private void Pulse()
        {
            if (alreadyPulsed || pulseSetting == null) return;

            SenderAssembly.Instance.Send(pulseSetting.OscAddress, pulseSetting.OscData);

            alreadyPulsed = true;
        }

    }

}

