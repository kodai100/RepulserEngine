using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using UniRx;
using Zenject;

namespace ProjectBlue.RepulserEngine.View
{

    public class PulseSettingListView : ReorderableListView<PulseSettingView, PulseSetting>
    {

        [Inject] private IPulseSettingListPresenter pulseSettingListPresenter;
        
        protected override void OnSaveButtonClicked(IEnumerable<PulseSetting> items)
        {
            pulseSettingListPresenter.Save(items);
        }

        protected override void StartInternal()
        {
            SetData(pulseSettingListPresenter.Load());
        }

    }
    
}