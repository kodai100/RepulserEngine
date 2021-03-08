using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
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

        protected override void StartInternal()
        {
            SetData(commandSettingListPresenter.Load());
        }
    }
    
}