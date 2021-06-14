using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class CommandButtonView : MonoBehaviour
    {
        [Inject] private ICommandTriggerController commandTriggerController;

        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;

        private CommandSetting myCommand;

        public void Initialize(string buttonText, CommandSetting command)
        {
            SetButtonText(buttonText);
            myCommand = command;

            button.OnClickAsObservable().Subscribe(_ => { commandTriggerController.Send(command); }).AddTo(this);
        }

        public void SetButtonText(string buttonText)
        {
            text.text = buttonText;
        }
    }
}