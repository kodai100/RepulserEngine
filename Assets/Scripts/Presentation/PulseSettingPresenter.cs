using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseSettingPresenter : ReorderableListComponentPresenter<PulseSettingView, PulseSetting>
    {

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

                UpdateData();
                
            }).AddTo(this);
        }

        private void Update()
        {
            
            // PulseSetting is registered when save button is pressed
            if(Data == null) return;

            if (Data.PulseState == PulseState.Predecessor)
            {
                reorderableListComponentView.SetBefore();
            }
            else if (Data.PulseState == PulseState.Successor)
            {
                reorderableListComponentView.SetAfter();
            }
            
        }

        public override void UpdateData()
        {

            if (Data == null)
            {
                Data = new PulseSetting(
                    reorderableListComponentView.oscAddressField.text,
                    reorderableListComponentView.oscDataField.text,
                    new Timecode
                    {
                        dropFrame = false,
                        frame = Validate(reorderableListComponentView.timecodeFrameInputField.text),
                        hour = Validate(reorderableListComponentView.timecodeHourInputField.text),
                        minute = Validate(reorderableListComponentView.timecodeMinuteInputField.text),
                        second = Validate(reorderableListComponentView.timecodeSecondInputField.text),
                    }
                );
            }
            else
            {
                Data.UpdateData(
                    reorderableListComponentView.oscAddressField.text,
                    reorderableListComponentView.oscDataField.text,
                    new Timecode
                    {
                        dropFrame = false,
                        frame = Validate(reorderableListComponentView.timecodeFrameInputField.text),
                        hour = Validate(reorderableListComponentView.timecodeHourInputField.text),
                        minute = Validate(reorderableListComponentView.timecodeMinuteInputField.text),
                        second = Validate(reorderableListComponentView.timecodeSecondInputField.text),
                    }
                );
            }
            
        }

        private int Validate(string numberString)
        {
            return int.TryParse(numberString, out var result) ? result : 0;
        }

        public void SetBackgroundSaved()
        {
            reorderableListComponentView.SetSaved();
        }
    }

}

