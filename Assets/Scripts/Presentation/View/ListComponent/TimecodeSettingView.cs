using ProjectBlue.RepulserEngine.Domain.Model;
using TMPro;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class TimecodeSettingView : ReorderableListComponentView<TimecodeSetting>
    {

        [SerializeField] private TMP_InputField hourField;
        [SerializeField] private TMP_InputField minuteField;
        [SerializeField] private TMP_InputField secondField;
        [SerializeField] private TMP_InputField frameField;

        [SerializeField] private TMP_Dropdown dropdown;
        
        private TimecodeSetting data;

        private void Start()
        {
            Observable.Merge(
                hourField.OnValueChangedAsObservable().Skip(1),
                minuteField.OnValueChangedAsObservable().Skip(1),
                secondField.OnValueChangedAsObservable().Skip(1),
                frameField.OnValueChangedAsObservable().Skip(1),
                dropdown.OnValueChangedAsObservable().Skip(1).Select(value => value.ToString())
            )
            .Subscribe(value =>
            {
                SetDirty();

                data = ParseData(hourField.text, minuteField.text, secondField.text, frameField.text, dropdown.options[dropdown.value].text);
                if (data == null)
                {
                    Invalid();
                }

            }).AddTo(this);
        }

        public override void UpdateView(TimecodeSetting data)
        {
            if (data == null)
            {
                hourField.text = data.TimecodeData.hour.ToString();
                minuteField.text = data.TimecodeData.minute.ToString();
                secondField.text = data.TimecodeData.second.ToString();
                frameField.text = data.TimecodeData.frame.ToString();
                return;
            }
            
            this.data = data;
            hourField.text = data.TimecodeData.hour.ToString();
            minuteField.text = data.TimecodeData.minute.ToString();
            secondField.text = data.TimecodeData.second.ToString();
            frameField.text = data.TimecodeData.frame.ToString();
            // dropdown.value = data.ConnectedCommandId
        }

        private TimecodeSetting ParseData(string hour, string minute, string second, string frame, string command)
        {

            if (int.TryParse(hour, out var hourParsed) 
                && int.TryParse(minute, out var minuteParsed)
                && int.TryParse(second, out var secondParsed)
                && int.TryParse(frame, out var frameParsed))
            {
                return new TimecodeSetting(new TimecodeData(hourParsed, minuteParsed, secondParsed, frameParsed), command);
            }

            return null;
        }

        public override TimecodeSetting GetData()
        {
            return data;
        }
    }
    
}