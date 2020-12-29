using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class TimecodeListPresenter : ITimecodeSettingListPresenter
    {

        private ITimecodeSettingListView<TimecodeSetting> timecodeSettingListView;

        public IObservable<IEnumerable<TimecodeSetting>> OnSaveAsObservable => timecodeSettingListView.OnSavedAsObservable;

        public TimecodeListPresenter(ITimecodeSettingListView<TimecodeSetting> timecodeSettingListView)
        {
            this.timecodeSettingListView = timecodeSettingListView;
        }
        
        
        public void SetData(IEnumerable<TimecodeSetting> settingList)
        {
            timecodeSettingListView.SetData(settingList);
        }
    }
}