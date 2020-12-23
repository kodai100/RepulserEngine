using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ICommandSettingListPresenter
    {
        IObservable<IEnumerable<CommandSetting>> OnSaveAsObservable { get; }
        // 
        void SetData(IEnumerable<CommandSetting> settingList);
    }
}