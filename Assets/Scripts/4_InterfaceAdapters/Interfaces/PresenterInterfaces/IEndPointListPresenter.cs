using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IEndPointListPresenter
    {
        void Save(IEnumerable<EndpointSettingViewModel> settingList);
        IEnumerable<EndpointSettingViewModel> Load();
    }
}