using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IEndPointListController
    {
        void Update(IEnumerable<EndpointSettingViewModel> settingList);
        void Save();
        IEnumerable<EndpointSettingViewModel> Load();
        IObservable<IEnumerable<EndpointSettingViewModel>> OnListRecreatedAsObservable { get; }
    }
}