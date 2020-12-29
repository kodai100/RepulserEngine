using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using TMPro;
using UnityEngine;
using UniRx;
using System.Linq;

namespace ProjectBlue.RepulserEngine.View
{
    
    public class CommandSettingView : ReorderableListComponentView<CommandSetting>
    {

        [SerializeField] private TMP_InputField commandIdText;

        [SerializeField] private TMP_InputField commandText;

        [SerializeField] private TMP_InputField memoText;

        [SerializeField] private TMP_Dropdown modeDropdown;

        private CommandSetting data;

        private void Awake()
        {
            var array = Enum.GetValues(typeof(CommandType));
            var list = (from object item in array select item.ToString()).ToList();
            modeDropdown.ClearOptions();
            modeDropdown.AddOptions(list);
        }
        
        private void Start()
        {
            Observable.Merge(
                commandIdText.OnValueChangedAsObservable().Skip(1),
                commandText.OnValueChangedAsObservable().Skip(1),
                memoText.OnValueChangedAsObservable().Skip(1),
                modeDropdown.OnValueChangedAsObservable().Select(value => value.ToString()).Skip(1)
            )
            .Subscribe(value =>
            {
                SetDirty();

                data = ParseData(modeDropdown.value, commandIdText.text, commandText.text, memoText.text);

            }).AddTo(this);
            
        }
        
        private CommandSetting ParseData(int mode, string eventText, string commandText, string memoText)
        {
            return new CommandSetting(mode, eventText, commandText, memoText);
        }

        public override void UpdateView(CommandSetting commandsetting)
        {
            this.data = commandsetting;
            
            if (commandsetting == null)
            {
                modeDropdown.value = 0;
                commandIdText.text = "";
                commandText.text = "";
                memoText.text = "";
                return;
            }
            
            modeDropdown.value = commandsetting.CommandType;
            commandIdText.text = commandsetting.CommandId;
            commandText.text = commandsetting.Command;
            memoText.text = commandsetting.Memo;
        }

        public override CommandSetting GetData()
        {
            return data;
        }
    }

}