using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Controllers;
using ProjectBlue.RepulserEngine.Domain.ViewModel;
using ProjectBlue.RepulserEngine.View;
using Zenject;

public class EndPointSettingListView : ReorderableListView<EndpointSettingView, EndpointSettingViewModel>
{
    [Inject] private IEndPointListController _endPointListController;

    protected override void OnSaveButtonClicked(IEnumerable<EndpointSettingViewModel> items)
    {
        _endPointListController.Save();
        // 保存時にテストOSCを送信するようにする
    }

    protected override void OnUpdateList(IEnumerable<EndpointSettingViewModel> items)
    {
        _endPointListController.Update(items);
    }

    protected override void Start()
    {
        base.Start();
        RecreateAllItem(_endPointListController.Load());
    }
}