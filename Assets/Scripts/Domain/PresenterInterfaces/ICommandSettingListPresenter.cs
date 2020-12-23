using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ICommandSettingListPresenter
    {
        IEnumerable<CommandSetting> CommandSettingList { get; }
        IEnumerable<ICommandSettingPresenter> CommandSettingPresenterList { get; }
        IObservable<Unit> OnSaveButtonClickedAsObservable { get; }

        void SetData(IEnumerable<CommandSetting> settingList);
    }
}