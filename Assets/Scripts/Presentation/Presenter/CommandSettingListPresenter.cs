using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class CommandSettingListPresenter : ICommandSettingListPresenter
    {
        
        private ICommandSettingListView<CommandSetting> commandSettingListView;
        public IObservable<IEnumerable<CommandSetting>> OnSaveAsObservable => commandSettingListView.OnSaveAsObservable;
        
        // public IObservable<int> OnSendAsObservable { get; }
        
        public CommandSettingListPresenter(ICommandSettingListView<CommandSetting> commandSettingListView)
        {
            this.commandSettingListView = commandSettingListView;
        }
        
        public void SetData(IEnumerable<CommandSetting> settingList)
        {
            commandSettingListView.SetData(settingList);
        }
    }

}