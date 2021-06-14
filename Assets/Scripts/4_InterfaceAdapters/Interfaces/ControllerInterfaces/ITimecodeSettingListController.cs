using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface ITimecodeSettingListController
    {
        void Save(IEnumerable<TimecodeSetting> settingList);
        IEnumerable<TimecodeSetting> Load();
    }
}