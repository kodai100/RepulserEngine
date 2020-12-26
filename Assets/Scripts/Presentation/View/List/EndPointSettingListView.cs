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
        // Saveボタンが押されたときに、すべてのリスト要素の内容をバリデーションして、通ったらセーブするために上流に流す。
        saveButton.OnClickAsObservable().Subscribe(_ =>
        {
            // TODO validate
            // if(validate == false) return;

            onSaveSubject.OnNext(ReorderedComponentList.Select(component =>
            {
                var endPoint = new IPEndPoint(IPAddress.Parse(component.ipTextField.text), int.Parse(component.portTextField.text));
                return new EndpointSetting(endPoint, "",0);
            }));
            
            foreach (var component in ReorderedComponentList)
            {
                component.SetBackgroundSaved();
            }

        }).AddTo(this);
    }
}
