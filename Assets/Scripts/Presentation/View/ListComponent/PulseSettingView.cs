using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace ProjectBlue.RepulserEngine.View
{
    
    public class PulseSettingView : ReorderableListComponentView<PulseSetting>
    {
        
        [SerializeField] protected Button sendButton;

        // TODO capsule
        [SerializeField] public InputField oscAddressField;
        [SerializeField] public InputField oscDataField;

        [SerializeField] public InputField timecodeHourInputField;
        [SerializeField] public InputField timecodeMinuteInputField;
        [SerializeField] public InputField timecodeSecondInputField;
        [SerializeField] public InputField timecodeFrameInputField;

        [SerializeField] public InputField overrideIpInputField;
        [SerializeField] public InputField sendkeyInputField;
        
        [SerializeField] private Color beforeColor = new Color(0, 0.5f, 0.5f);
        [SerializeField] private Color afterColor = new Color(0.2f, 0.2f, 0.2f);

        public IObservable<Unit> OnSendButtonClickedAsObservable => sendButton.OnClickAsObservable();

        private enum State
        {
            Initialize,
            Before,
            Pulse,
            After,
            Edited,
            Saved
        }

        private State state = State.Initialize;
        
        private void Start()
        {
            Observable.Merge(
                timecodeHourInputField.OnValueChangedAsObservable().Skip(1),
                timecodeMinuteInputField.OnValueChangedAsObservable().Skip(1),
                timecodeSecondInputField.OnValueChangedAsObservable().Skip(1),
                timecodeFrameInputField.OnValueChangedAsObservable().Skip(1),
                overrideIpInputField.OnValueChangedAsObservable().Skip(1),
                sendkeyInputField.OnValueChangedAsObservable().Skip(1),
                oscAddressField.OnValueChangedAsObservable().Skip(1),
                oscDataField.OnValueChangedAsObservable().Skip(1)
            ).Subscribe(value =>
            {
                SetEdited();
            }).AddTo(this);
            
        }

        // Validate input and create data
        public PulseSetting CreateData()
        {

            try
            {
                var hour = int.Parse(timecodeHourInputField.text);
                var minute = int.Parse(timecodeMinuteInputField.text);
                var second = int.Parse(timecodeSecondInputField.text);
                var frame = int.Parse(timecodeFrameInputField.text);

                var timecode = new Timecode{dropFrame = false, hour = hour, minute = minute, second = second, frame = frame};
                return new PulseSetting(oscAddressField.text, oscDataField.text, timecode, overrideIpInputField.text, sendkeyInputField.text);
            }
            catch
            {
                return null;
            }
            
        }

        public void UpdateTimecode(Timecode timecode)
        {
            
        }

        public override void UpdateView(PulseSetting pulseSetting)
        {
            oscAddressField.text = pulseSetting.OscAddress;
            oscDataField.text = pulseSetting.OscData;

            timecodeHourInputField.text = pulseSetting.Timecode.hour.ToString();
            timecodeMinuteInputField.text = pulseSetting.Timecode.minute.ToString();
            timecodeSecondInputField.text = pulseSetting.Timecode.second.ToString();
            timecodeFrameInputField.text = pulseSetting.Timecode.frame.ToString();

            overrideIpInputField.text = pulseSetting.OverrideIp;
            sendkeyInputField.text = pulseSetting.SendKey;
        }

        public void SetBefore()
        {
            if (state == State.Before || state == State.Edited) return;
            backgroundImage.color = beforeColor;
            state = State.Before;

        }

        public void SetSaved()
        {
            state = State.Saved;
            backgroundImage.color = beforeColor;
        }

        public void SetAfter()
        {
            if (state == State.After || state == State.Edited) return;
            backgroundImage.color = afterColor;
            state = State.After;
        }

        public void SetEdited()
        {
            if (state == State.Edited) return;
            backgroundImage.color = Color.red;
            state = State.Edited;
        }

    }

}