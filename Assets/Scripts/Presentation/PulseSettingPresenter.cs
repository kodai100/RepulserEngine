using Ltc;
using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseSettingPresenter : ReorderableListComponentPresenter<PulseSettingView>
    {

        public bool AlreadyPulsed { get; private set; } = false;

        public PulseSetting PulseSetting { get; private set; } = null;

        public IObservable<Unit> OnUpButtonClickedAsObservable => reorderableListComponentView.OnUpButtonClickedAsObservable;
        public IObservable<Unit> OnDownButtonClickedAsObservable => reorderableListComponentView.OnDownButtonClickedAsObservable;

        private Timecode prevTimecode;

        private void Start()
        {
            Observable.Merge(
                reorderableListComponentView.OscAddressAsObservable,
                reorderableListComponentView.OscDataAsObservable,
                reorderableListComponentView.HourAsObservable,
                reorderableListComponentView.MinuteAsObservable,
                reorderableListComponentView.SecondAsObservable,
                reorderableListComponentView.FrameAsObservable
            ).Subscribe(value =>
            {
                reorderableListComponentView.SetEdited();
            }).AddTo(this);
        }

        public override void Load()
        {
            PulseSetting = PulseSetting.Load(Index);
            reorderableListComponentView.SetData(PulseSetting);
        }
        
        public override void Save()
        {
            PulseSetting = new PulseSetting(
                Index,
                reorderableListComponentView.oscAddressField.text,
                reorderableListComponentView.oscDataField.text,
                new Timecode
                {
                    dropFrame = false,
                    frame = int.Parse(reorderableListComponentView.timecodeFrameInputField.text),
                    hour = int.Parse(reorderableListComponentView.timecodeHourInputField.text),
                    minute = int.Parse(reorderableListComponentView.timecodeMinuteInputField.text),
                    second = int.Parse(reorderableListComponentView.timecodeSecondInputField.text),
                }
            );
            
            PulseSetting.Save();
            reorderableListComponentView.SetSaved();
        }

        public void Evaluate(Timecode timecode, Action<OscMessage> onPulse)
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

        private void Pulse(Action<OscMessage> onPulse)
        {
            if (AlreadyPulsed) return;

            var message = new OscMessage
            {
                OscAddress = PulseSetting.OscAddress,
                OscData = PulseSetting.OscData
            };

            onPulse?.Invoke(message);
            
        }

        public void SetBefore()
        {
            reorderableListComponentView.SetBefore();
            AlreadyPulsed = false;
        }
        
        public void SetAfter()
        {
            reorderableListComponentView.SetAfter();
            AlreadyPulsed = true;
        }

        public void SetPulsed()
        {
            AlreadyPulsed = true;
        }
        
    }

}

