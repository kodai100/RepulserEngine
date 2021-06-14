using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{
    public class CommandSettingListView : ReorderableListView<CommandSettingView, CommandSetting>
    {
        [Inject] private ICommandSettingListController commandSettingListController;

        protected override void OnSaveButtonClicked(IEnumerable<CommandSetting> items)
        {
            commandSettingListController.Save(items);
        }

        protected override void OnUpdateList(IEnumerable<CommandSetting> items)
        {
            // TODO: 上流に変更伝える
        }

        protected override void Start()
        {
            base.Start();

            RecreateAllItem(commandSettingListController.Load());
        }
    }
}