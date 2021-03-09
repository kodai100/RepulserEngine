using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Presentation;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{

    public class CommandSettingListView : ReorderableListView<CommandSettingView, CommandSetting>
    {
        
        [Inject] private ICommandSettingListPresenter commandSettingListPresenter;
        
        protected override void OnSaveButtonClicked(IEnumerable<CommandSetting> items)
        {
            commandSettingListPresenter.Save(items);
        }

        protected override void OnUpdateList(IEnumerable<CommandSetting> items)
        {
            // TODO: 上流に変更伝える
        }

        protected override void StartInternal()
        {
            RecreateAllItem(commandSettingListPresenter.Load());
        }
    }
    
}