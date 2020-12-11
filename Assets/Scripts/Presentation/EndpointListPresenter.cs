using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class EndpointListPresenter : ListPresenter<EndpointSettingPresenter, EndpointSettingView>, IEndPointListPresenter
    {
        
        protected override string SaveHash => "Endpoint";

        public IEnumerable<EndpointSetting> EndpointSettingList
            => ComponentList.Select(presenter => new EndpointSetting(presenter.EndPoint, "", 0));
        
    }
}