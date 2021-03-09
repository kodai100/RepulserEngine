using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IEndPointListPresenter
    {
        void Update(IEnumerable<EndpointSettingViewModel> settingList);
        void Save();
        IEnumerable<EndpointSettingViewModel> Load();
        IObservable<IEnumerable<EndpointSettingViewModel>> OnListRecreatedAsObservable { get; }
    }
}