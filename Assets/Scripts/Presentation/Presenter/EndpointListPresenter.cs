using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class EndpointListPresenter : IEndPointListPresenter
    {

        private IEndPointSettingListView<EndpointSetting> _endPointSettingListView;

        public IObservable<IEnumerable<EndpointSetting>> OnSaveAsObservable => _endPointSettingListView.OnSavedAsObservable;

        public EndpointListPresenter(IEndPointSettingListView<EndpointSetting> endPointSettingListView)
        {
            this._endPointSettingListView = endPointSettingListView;
        }
        
        
        public void SetData(IEnumerable<EndpointSetting> settingList)
        {
            _endPointSettingListView.SetData(settingList);
        }
    }
}