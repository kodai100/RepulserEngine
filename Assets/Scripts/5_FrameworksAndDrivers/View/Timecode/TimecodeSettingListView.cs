using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Presentation;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class TimecodeSettingListView : ReorderableListView<TimecodeSettingView, TimecodeSetting>
    {

        [Inject] private ITimecodeSettingListPresenter timecodeSettingListPresenter;
    
        protected override void OnSaveButtonClicked(IEnumerable<TimecodeSetting> items)
        {
            timecodeSettingListPresenter.Save(items);
            Debug.Log("Clicked");
        }

        protected override void OnUpdateList(IEnumerable<TimecodeSetting> items)
        {
            // TODO: 上流に変更伝える
        }

        protected override void Start()
        {
            base.Start();
            RecreateAllItem(timecodeSettingListPresenter.Load());
        }
    }
}