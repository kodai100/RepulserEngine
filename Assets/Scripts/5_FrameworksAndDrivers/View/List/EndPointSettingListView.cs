using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.Presentation;
using ProjectBlue.RepulserEngine.View;
using Zenject;

public class EndPointSettingListView : ReorderableListView<EndpointSettingView, EndpointSetting>
{
    [Inject] private IEndPointListPresenter endPointListPresenter;

    protected override void OnSaveButtonClicked(IEnumerable<EndpointSetting> items)
    {
        endPointListPresenter.Save(items);
        
        // 保存時にテストOSCを送信するようにする
    }

    protected override void StartInternal()
    {
        SetData(endPointListPresenter.Load());
    }
    
}
