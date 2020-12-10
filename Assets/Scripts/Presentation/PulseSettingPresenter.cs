using Ltc;
using System;
using UniRx;
using Zenject;

namespace ProjectBlue.RepulserEngine
{
    public class PulseSettingPresenter : ListComponentPresenter<PulseSettingView>
    {

        public bool AlreadyPulsed { get; private set; } = false;

        public PulseSetting PulseSetting { get; private set; } = null;

        private Timecode prevTimecode;

        private void Start()
        {
            Observable.Merge(
                listComponentView.OscAddressAsObservable,
                listComponentView.OscDataAsObservable,
                listComponentView.HourAsObservable,
                listComponentView.MinuteAsObservable,
                listComponentView.SecondAsObservable,
                listComponentView.FrameAsObservable
            ).Subscribe(value =>
            {
                listComponentView.SetEdited();
            }).AddTo(this);
        }

        public override void Load(int index)
        {
            PulseSetting = PulseSetting.Load(index);
            listComponentView.SetData(PulseSetting);
        }
        
        public override void Save(int index)
        {
            PulseSetting = new PulseSetting(
                index,
                listComponentView.oscAddressField.text,
                listComponentView.oscDataField.text,
                new Timecode
                {
                    dropFrame = false,
                    frame = int.Parse(listComponentView.timecodeFrameInputField.text),
                    hour = int.Parse(listComponentView.timecodeHourInputField.text),
                    minute = int.Parse(listComponentView.timecodeMinuteInputField.text),
                    second = int.Parse(listComponentView.timecodeSecondInputField.text),
                }
            );
            
            PulseSetting.Save();
            listComponentView.SetSaved();
        }

        public void Evaluate(Timecode timecode, Action<Message> onPulse)
        {
            if (prevTimecode == timecode || PulseSetting == null) return;

            if (timecode < PulseSetting.Timecode)
            {
                SetBefore();
            }

            if (timecode == PulseSetting.Timecode)
            {
                Pulse(onPulse);
            }

            if (PulseSetting.Timecode < timecode)
            {
                SetAfter();
            }

            prevTimecode = timecode;
        }

        private void Pulse(Action<Message> onPulse)
        {
            if (AlreadyPulsed) return;

            var message = new Message
            {
                OscAddress = PulseSetting.OscAddress,
                OscData = PulseSetting.OscData
            };

            onPulse?.Invoke(message);
            
        }

        public void SetBefore()
        {
            listComponentView.SetBefore();
            AlreadyPulsed = false;
        }
        
        public void SetAfter()
        {
            listComponentView.SetAfter();
            AlreadyPulsed = true;
        }

        public void SetPulsed()
        {
            AlreadyPulsed = true;
        }
        
    }

}

