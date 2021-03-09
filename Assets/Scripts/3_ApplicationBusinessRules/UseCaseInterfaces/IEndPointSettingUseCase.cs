using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.DataModel;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface IEndPointSettingUseCase
    {
        IObservable<IEnumerable<EndpointSetting>> OnDataChangedAsObservable { get; }
        void Save(IEnumerable<EndpointSetting> settings);
        IEnumerable<EndpointSetting> Load();
    }

}