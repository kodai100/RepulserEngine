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

        public IObservable<string> OscAddressAsObservable => oscAddressField.OnValueChangedAsObservable().Skip(1);
        public IObservable<Unit> OnPauseButtonClickedAsObservable => pauseButton.OnClickAsObservable();
        public IObservable<Unit> OnStopButtonClickedAsObservable => stopButton.OnClickAsObservable();
        public IObservable<Unit> OnBackButtonClickedAsObservable => backButton.OnClickAsObservable();
        public IObservable<Unit> OnSkipButtonClickedAsObservable => skipButton.OnClickAsObservable();


        private void Start()
        {
            oscAddressField.OnValueChangedAsObservable().Subscribe(value =>
            {
                SetDirty();
            }).AddTo(this);
        }

        public override void UpdateView(CommandSetting commandsetting)
        {
            oscAddressField.text = commandsetting.OscAddress;
        }

    }

}