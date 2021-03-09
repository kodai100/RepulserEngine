using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.View;
using Zenject;

public class EndPointSettingListView : ReorderableListView<EndpointSettingView, EndpointSettingDataModel>
{
    [Inject] private IEndPointListPresenter endPointListPresenter;

    protected override void OnSaveButtonClicked(IEnumerable<EndpointSettingDataModel> items)
    {
        endPointListPresenter.Save(items);
        
        // 保存時にテストOSCを送信するようにする
    }

    protected override void StartInternal()
    {
        SetData(endPointListPresenter.Load());
    }
    
}
