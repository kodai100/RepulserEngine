using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface IEndPointListPresenter
    {
        void Save(IEnumerable<EndpointSetting> settingList);
        IEnumerable<EndpointSetting> Load();
        IObservable<IEnumerable<EndpointSetting>> OnDataChangedAsObservable { get; }
    }
}