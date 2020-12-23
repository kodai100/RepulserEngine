using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseSettingListPresenter : IPulseSettingListPresenter
    {
        private IPulseSettingListView<PulseSetting> pulseSettingListView;

        public IObservable<IEnumerable<PulseSetting>> OnSaveAsObservable => pulseSettingListView.OnSaveAsObservable;
        public IObservable<int> OnSendAsObservable { get; }
        
        public PulseSettingListPresenter(IPulseSettingListView<PulseSetting> pulseSettingListView)
        {
            this.pulseSettingListView = pulseSettingListView;
        }
        
        public void SetData(IEnumerable<PulseSetting> settingList)
        {
            pulseSettingListView.SetData(settingList);
        }
    }

}