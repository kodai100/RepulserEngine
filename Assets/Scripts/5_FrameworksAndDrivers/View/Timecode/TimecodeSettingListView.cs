using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class TimecodeSettingListView : ReorderableListView<TimecodeSettingView, TimecodeSetting>
    {
        [Inject] private ITimecodeSettingListController _timecodeSettingListController;

        protected override void OnSaveButtonClicked(IEnumerable<TimecodeSetting> items)
        {
            _timecodeSettingListController.Save(items);
        }

        protected override void OnUpdateList(IEnumerable<TimecodeSetting> items)
        {
            // TODO: 上流に変更伝える
        }

        protected override void Start()
        {
            base.Start();
            RecreateAllItem(_timecodeSettingListController.Load());
        }
    }
}