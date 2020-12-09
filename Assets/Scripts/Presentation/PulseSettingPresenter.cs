using Ltc;
using UniRx;

namespace ProjectBlue.RepulserEngine
{
    public class PulseSettingPresenter : ListComponentPresenter<PulseSettingView>
    {

        public bool AlreadyPulsed { get; set; } = false;

        public PulseSetting PulseSetting { get; private set; } = null;

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

        public void SetBefore()
        {
            listComponentView.SetBefore();
        }
        
        public void SetAfter()
        {
            listComponentView.SetAfter();
        }

        public void SetPulsed()
        {
            
        }
        
    }

}

