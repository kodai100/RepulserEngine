using System;
using Ltc;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class CommandSettingPresenter : ReorderableListComponentPresenter<CommandSettingView, CommandSetting>, ICommandSettingPresenter
    {
        public CommandSetting CommandSetting => Data;
        
        public IObservable<Unit> OnPauseButtonClickedAsObservable => reorderableListComponentView.OnPauseButtonClickedAsObservable;
        public IObservable<Unit> OnStopButtonClickedAsObservable => reorderableListComponentView.OnStopButtonClickedAsObservable;
        public IObservable<Unit> OnBackButtonClickedAsObservable => reorderableListComponentView.OnBackButtonClickedAsObservable;
        public IObservable<Unit> OnSkipButtonClickedAsObservable => reorderableListComponentView.OnSkipButtonClickedAsObservable;
        
        
        

        private void Start()
        {
            Observable.Merge(
                reorderableListComponentView.OscAddressAsObservable
            ).Subscribe(value =>
            {
                reorderableListComponentView.SetEdited();

                UpdateData();
                
            }).AddTo(this);
        }

        private void Update()
        {
            
            // CommandSetting is registered when save button is pressed
            if(Data == null) return;

            if (Data.CommandState == CommandState.Predecessor)
            {
                reorderableListComponentView.SetBefore();
            }
            else if (Data.CommandState == CommandState.Successor)
            {
                reorderableListComponentView.SetAfter();
            }
            
        }

        public override void UpdateData()
        {

            if (Data == null)
            {
                Data = new CommandSetting(
                    reorderableListComponentView.oscAddressField.text
                );
            }
            else
            {
                Data.UpdateData(
                    reorderableListComponentView.oscAddressField.text
                );
            }
            
        }

        private int Validate(string numberString)
        {
            return int.TryParse(numberString, out var result) ? result : 0;
        }

        public void SetBackgroundSaved()
        {
            reorderableListComponentView.SetSaved();
        }
    }

}

