using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface ICommandSettingListController
    {
        void Save(IEnumerable<CommandSetting> settingList);
        IEnumerable<CommandSetting> Load();
        IObservable<IEnumerable<CommandSetting>> OnListChangedAsObservable { get; }
    }
}