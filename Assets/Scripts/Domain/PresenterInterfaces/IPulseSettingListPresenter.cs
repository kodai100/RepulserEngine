using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IPulseSettingListPresenter
    {
        IObservable<IEnumerable<PulseSetting>> OnSaveAsObservable { get; }
        IObservable<int> OnSendAsObservable { get; }
        void SetData(IEnumerable<PulseSetting> settingList);
    }
}