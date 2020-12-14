using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IPulseSettingListPresenter
    {
        IEnumerable<PulseSetting> PulseSettingList { get; }
        IEnumerable<IPulseSettingPresenter> PulseSettingPresenterList { get; }
        IObservable<Unit> OnSaveButtonClickedAsObservable { get; }

        void SetData(IEnumerable<PulseSetting> settingList);
    }
}