using Ltc;
using UniRx;

namespace ProjectBlue.RepulserEngine
{
    public class PulseSettingPresenter : ListComponentPresenter<PulseSettingView>
    {
        private bool alreadyPulsed = false;

        private PulseSetting pulseSetting = null;

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

        public void Evaluate(Timecode timecode)
        {
            if (pulseSetting == null) return;

            if (timecode < pulseSetting.Timecode)
            {
                alreadyPulsed = false;
                listComponentView.SetBefore();
            }
            
            if (timecode == pulseSetting.Timecode)
            {
                listComponentView.SetPulse();
                Pulse();
            }
            
            if (pulseSetting.Timecode < timecode)
            {
                listComponentView.SetAfter();
            }
        }

        public override void Load(int index)
        {
            pulseSetting = PulseSetting.Load(index);
            listComponentView.SetData(pulseSetting);
        }
        
        public override void Save(int index)
        {
            pulseSetting = new PulseSetting(
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
            
            pulseSetting.Save();
            listComponentView.SetSaved();
        }
        
        private void Pulse()
        {
            if (alreadyPulsed || pulseSetting == null) return;

            // SendDestinationListPresenter.Instance.Send(pulseSetting.OscAddress, pulseSetting.OscData);

            alreadyPulsed = true;
        }

    }

}

