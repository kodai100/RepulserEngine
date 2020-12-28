using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ProjectBlue.RepulserEngine.Domain.Model;
using ProjectBlue.RepulserEngine.View;
using UniRx;

public class EndPointSettingListView : ReorderableListView<EndpointSettingView, EndpointSetting>, IEndPointSettingListView<EndpointSetting>
{
    public IObservable<IEnumerable<EndpointSetting>> OnSaveAsObservable => onSaveSubject;
    
    private Subject<IEnumerable<EndpointSetting>> onSaveSubject = new Subject<IEnumerable<EndpointSetting>>();
    
    protected override void StartInternal()
    {
        
    }
}
