using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class EndpointListPresenter : ListPresenter<EndpointSettingPresenter, EndpointSettingView, EndpointSetting>, IEndPointListPresenter
    {
        public IEnumerable<EndpointSetting> EndpointSettingList => ComponentList.Select(presenter => presenter.Data);
    }
}