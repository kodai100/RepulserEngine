using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IPulseSettingListPresenter
    {
        void Save(IEnumerable<PulseSetting> settingList);
        IEnumerable<PulseSetting> Load();
    }
}