using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ITimecodeSettingListPresenter
    {
        IObservable<IEnumerable<TimecodeSetting>> OnSaveAsObservable { get; }
        void SetData(IEnumerable<TimecodeSetting> settingList);
    }
}