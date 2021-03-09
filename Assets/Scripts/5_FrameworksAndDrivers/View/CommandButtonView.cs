using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Presentation;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{

    public class CommandButtonView : MonoBehaviour
    {

        [Inject] private ISendCommandPresenter sendCommandPresenter;
        
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;

        private CommandSetting myCommand;
        
        public void Initialize(string buttonText, CommandSetting command)
        {
            SetButtonText(buttonText);
            myCommand = command;

            button.OnClickAsObservable().Subscribe(_ =>
            {
                sendCommandPresenter.Send(command);
            }).AddTo(this);
        }
        
        public void SetButtonText(string buttonText)
        {
            text.text = buttonText;
        }
    }

}