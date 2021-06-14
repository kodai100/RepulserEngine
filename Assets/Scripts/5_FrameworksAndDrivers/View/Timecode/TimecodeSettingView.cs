using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class TimecodeSettingView : ReorderableListComponentView<TimecodeSetting>
    {
        [Inject] private ICommandSettingListController _commandSettingController;

        [SerializeField] private TMP_InputField hourField;
        [SerializeField] private TMP_InputField minuteField;
        [SerializeField] private TMP_InputField secondField;
        [SerializeField] private TMP_InputField frameField;

        [SerializeField] private TMP_Dropdown dropdown;

        public override TimecodeSetting Data => data;
        private TimecodeSetting data = new TimecodeSetting();

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

                    data = ParseData(hourField.text, minuteField.text, secondField.text, frameField.text,
                        dropdown.options[dropdown.value].text);
                    if (data == null)
                    {
                        Invalid();
                    }
                }).AddTo(this);

            _commandSettingController.OnListChangedAsObservable.Subscribe(list =>
            {
                UpdateOptionList(list);
                UpdateCommandSelection();
            }).AddTo(this);

            UpdateOptionList(_commandSettingController.Load());
        }

        // プルダウンオプションの種類をCommandリストと同期
        private void UpdateOptionList(IEnumerable<CommandSetting> list)
        {
            var optionList = new List<TMP_Dropdown.OptionData>();

            optionList.Add(new TMP_Dropdown.OptionData("None"));

            optionList.AddRange(list
                .Select(commandSetting => new TMP_Dropdown.OptionData(commandSetting.CommandName))
                .ToList());

            dropdown.options = optionList;
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

            UpdateOptionList(_commandSettingController.Load());
            UpdateCommandSelection();
        }

        private void UpdateCommandSelection()
        {
            // 選択しているコマンド名をプルダウンインデックスに変換
            var result = 0;
            for (var i = 0; i < dropdown.options.Count; i++)
            {
                var optionText = dropdown.options[i].text;
                if (data.ConnectedCommandName == optionText)
                {
                    result = i;
                    break;
                }
            }

            dropdown.value = result;
        }

        private TimecodeSetting ParseData(string hour, string minute, string second, string frame, string command)
        {
            if (int.TryParse(hour, out var hourParsed)
                && int.TryParse(minute, out var minuteParsed)
                && int.TryParse(second, out var secondParsed)
                && int.TryParse(frame, out var frameParsed))
            {
                return new TimecodeSetting(new TimecodeData(hourParsed, minuteParsed, secondParsed, frameParsed, false),
                    command);
            }

            return null;
        }
    }
}