using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class PulseSettingListPresenter : ReorderableListPresenter<PulseSettingPresenter, PulseSettingView, PulseSetting>, IPulseSettingListPresenter
    {
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