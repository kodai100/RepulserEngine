using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.View;
using Zenject;

public class EndPointSettingListView : ReorderableListView<EndpointSettingView, EndpointSettingViewModel>
{
    [Inject] private IEndPointListPresenter endPointListPresenter;

    protected override void OnSaveButtonClicked(IEnumerable<EndpointSettingViewModel> items)
    {
        endPointListPresenter.Save();
        // 保存時にテストOSCを送信するようにする
    }

    protected override void OnUpdateList(IEnumerable<EndpointSettingViewModel> items)
    {
        endPointListPresenter.Update(items);
    }

    protected override void Start()
    {
        base.Start();
        RecreateAllItem(endPointListPresenter.Load());
    }
    
}
