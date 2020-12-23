using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class CommandSettingListPresenter : ReorderableListPresenter<CommandSettingPresenter, CommandSettingView, CommandSetting>, ICommandSettingListPresenter
    {
        
        public IEnumerable<CommandSetting> CommandSettingList => ReorderedComponentList.Select(presenter => presenter.Data);
        public IEnumerable<ICommandSettingPresenter> CommandSettingPresenterList => ReorderedComponentList.Select(presenter => presenter as ICommandSettingPresenter);
        
        protected override void StartInternal()
        {
            
            listView.OnSaveButtonClickedAsObservable.Subscribe(_ =>
            {
                foreach (var component in ReorderedComponentList)
                {
                    component.SetBackgroundSaved();
                }
                
            }).AddTo(this);
            
        }
    }

}