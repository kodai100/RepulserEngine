using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IEndPointListPresenter
    {
        IObservable<IEnumerable<EndpointSetting>> OnSaveAsObservable { get; }
        void SetData(IEnumerable<EndpointSetting> settingList);
    }
}