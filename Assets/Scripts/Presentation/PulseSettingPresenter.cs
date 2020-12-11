using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseSettingPresenter : ReorderableListComponentPresenter<PulseSettingView>
    {

        public PulseSetting PulseSetting { get; private set; } = null;

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

        private void Update()
        {
            
            // PulseSetting is registered when save button is pressed
            if(PulseSetting == null) return;

            if (PulseSetting.PulseState == PulseState.Predecessor)
            {
                reorderableListComponentView.SetBefore();
            }
            else if (PulseSetting.PulseState == PulseState.Successor)
            {
                reorderableListComponentView.SetAfter();
            }
            
        }

    }

}

