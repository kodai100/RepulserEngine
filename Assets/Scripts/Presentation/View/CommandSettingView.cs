using System;
using ProjectBlue.RepulserEngine.Domain.Model;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace ProjectBlue.RepulserEngine.View
{
    
    public class CommandSettingView : ReorderableListComponentView<CommandSetting>
    {
        
        [SerializeField] protected Button pauseButton;
        [SerializeField] protected Button stopButton;
        [SerializeField] protected Button backButton;
        [SerializeField] protected Button skipButton;

        


        // TODO capsule
        [SerializeField] public InputField oscAddressField;
       
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color beforeColor = new Color(0, 0.5f, 0.5f);
        [SerializeField] private Color afterColor = new Color(0.2f, 0.2f, 0.2f);

        public IObservable<string> OscAddressAsObservable => oscAddressField.OnValueChangedAsObservable().Skip(1);
               public IObservable<Unit> OnPauseButtonClickedAsObservable => pauseButton.OnClickAsObservable();
        public IObservable<Unit> OnStopButtonClickedAsObservable => stopButton.OnClickAsObservable();
        public IObservable<Unit> OnBackButtonClickedAsObservable => backButton.OnClickAsObservable();
        public IObservable<Unit> OnSkipButtonClickedAsObservable => skipButton.OnClickAsObservable();

        // private Material mat;

        public enum State
        {
            Initialize,
            Before,
            Pulse,
            After,
            Edited,
            Saved
        }

        private State state = State.Initialize;

        public override void SetData(CommandSetting commandsetting)
        {
            oscAddressField.text = commandsetting.OscAddress;
         
         
         
        }

        public void SetBefore()
        {
            if (state == State.Before || state == State.Edited) return;
            backgroundImage.color = beforeColor;
            state = State.Before;

        }

        public void SetSaved()
        {
            state = State.Saved;
            backgroundImage.color = beforeColor;
        }

        public void SetAfter()
        {
            if (state == State.After || state == State.Edited) return;
            backgroundImage.color = afterColor;
            state = State.After;
        }

        public void SetEdited()
        {
            if (state == State.Edited) return;
            backgroundImage.color = Color.red;
            state = State.Edited;
        }

    }

}