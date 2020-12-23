using System.Collections.Generic;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseSettingListPresenter : ReorderableListPresenter<PulseSettingPresenter, PulseSettingView, PulseSetting>, IPulseSettingListPresenter
    {
        
        public IEnumerable<PulseSetting> PulseSettingList => ReorderedComponentList.Select(presenter => presenter.Data);
        public IEnumerable<IPulseSettingPresenter> PulseSettingPresenterList => ReorderedComponentList.Select(presenter => presenter as IPulseSettingPresenter);
        
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